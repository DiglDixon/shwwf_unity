using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

/// <summary>
/// Used to play a playlist through a variety of TrackOutputs
/// </summary>
public class TracklistPlayer : WrappedTrackOutput{
	
	public MultiTrackOutput multiPlayer;

	public ActSet actSet;

	public VideoPlaybackSystem videoSystem;

	private TrackOutput currentOutput;

	private List<ITrack> loadedTracks = new List<ITrack> ();

	private List<ITrack> preserveTrackLoads = new List<ITrack> ();

	public Tracklist tracklist;
	private int trackIndex = 0;

	public delegate void NewTrackBeginsDelegate (ITrack track);
	public event NewTrackBeginsDelegate NewTrackBeginsEvent;

//	private delegate void TrackEndsDelegate (ITrack track);
//	private event TrackEndsDelegate TrackEndsEvent;

	private void Awake(){
		currentOutput = multiPlayer;
	}

	protected override TrackOutput WrappedOutput{
		get{
			return currentOutput;
		}
	}

	protected void Start(){
		SetTracklist (tracklist);
	}

	public void SendExpectedActWhenLoaded(){
		Payload p = BLE.Instance.Manager.GetExpectedPayload ();
		Act a = actSet.GetActForPayload (p);
		StopCoroutine ("SendWhenLoaded");
		StartCoroutine ("SendWhenLoaded", new ActBoolBundle(a, false));
	}

	public void SendCustomActWhenLoaded(Act a){
		StopCoroutine ("SendWhenLoaded");
		StartCoroutine ("SendWhenLoaded", new ActBoolBundle(a, true));
	}

	private IEnumerator SendWhenLoaded(ActBoolBundle abb){
		Act a = abb.act;
		bool forced = abb.b;
		TracklistEntry toPlay = a.GetFirstTracklistEntry ();
//		SetTrack (toPlay.GetTrack ());
		LoadTrackIfNeeded (toPlay.GetTrack());
		while (!toPlay.GetTrack ().IsLoaded ()) {
			yield return null;
		}
		if (forced) {
			Payload toSend = actSet.GetPayloadForDefinedAct (a.definedAct);
			Diglbug.Log ("Forcing Payload Send from TracklistPlayer " + toSend);
			BLE.Instance.Manager.ForceSendPayload (toSend);
		} else {
			Diglbug.Log ("Sending Expected from TracklistPlayer ");
			BLE.Instance.Manager.SendExpectedPayload ();
		}
	}

	public void BeginActFromSignal(Act a, Signal s){
		StopCoroutine ("PlayActWhenLoaded");
		StartCoroutine ("PlayActWhenLoaded", new ActSignalBundle(a, s));
	}

	private IEnumerator PlayActWhenLoaded(ActSignalBundle asb){
		Act a = asb.act;
		Signal s = asb.signal;
		int timeToSkip = SignalUtils.GetSignalTimeOffset (s.GetSignalTime());
		TracklistEntry toPlay = a.GetEntryAtActTime (timeToSkip);

		LoadTrackIfNeeded (toPlay.GetTrack());
		while (!toPlay.GetTrack ().IsLoaded ()) {
			yield return null;
		}
		RecoveryManager.Instance.RecoveryComplete ();// TODO: Move this somewhere nicer. Maybe sub.

		int postLoadTimeToSkip = SignalUtils.GetSignalTimeOffset (s.GetSignalTime());
		float timeToPlayFrom = a.GetSpecificEntryTimeAtActTime (toPlay, postLoadTimeToSkip);

		PlayTrackEntry (toPlay, timeToPlayFrom);
	}

	public void AddPreservedTrack(ITrack track){
		Diglbug.Log("Added Preserved track "+track.GetTrackName(), PrintStream.AUDIO_GENERAL);
		preserveTrackLoads.Add (track);
	}

	public void ClearPreservedTracks(){
		Diglbug.Log("Cleared "+preserveTrackLoads.Count+" Preserved tracks", PrintStream.AUDIO_GENERAL);
		preserveTrackLoads.Clear ();
	}

	private void SetTracklist(Tracklist t){
		this.tracklist = t;
		TracklistEntry[] entries = tracklist.entries;
		EventTracklistEntry entry;
		TracklistEntry nextEntry;
		for(int k = 0; k<entries.Length; k++){
			entry = (EventTracklistEntry) entries [k];
			if (entry is LoopingTracklistEntry) {
				LoopingTrack loopingTrack = (LoopingTrack)entry.GetTrack ();
				entry.AddStateEventAtTimeRemaining (LoopCurrentTrack, loopingTrack.crossoverTime);
			} else if (k < entries.Length-1){
				nextEntry = entries [k + 1];
				entry.AddStateEventAtTimeRemaining (PlayNextTrack, nextEntry.GetTrack ().EntranceFadeTime ());
			}
			entry.AddStateEventAtTime (UnloadPreviousTrack, 4f);
			entry.AddStateEventAtTime (LoadNextTrack, 5f);
		}
		#if !UNITY_EDITOR
		((MobileVideoPlayer) videoSystem.GetPlayer()).InitialiseMobileVideoTracksInList(tracklist);
		#endif
	}

	public void LoadNextTrack(int ahead){
		if (trackIndex < tracklist.entries.Length - 1) {
			LoadTrackIfNeeded(GetNextTrack(ahead));
		}
	}

	public void LoadNextTrack(){
		LoadNextTrack (1);
	}

	public ITrack GetNextTrack(){
		return GetNextTrack (1);
	}

	public ITrack GetNextTrack(int increment){
		if (trackIndex >= tracklist.entries.Length-increment) {
			return null;
		} else {
			return tracklist.entries [trackIndex + increment].GetTrack ();
		}
	}

	public void LoadTrackIfNeeded(ITrack toLoad){
		// this call contains its own shouldLoad logic.
		toLoad.Load ();
		if (!loadedTracks.Contains (toLoad)) {
			loadedTracks.Add (toLoad);
			Diglbug.Log ("Added track to loadedTracks " + toLoad.GetTrackName (), PrintStream.MEDIA_LOAD);
		} else {
			Diglbug.Log ("Avoided adding a duplicate track "+toLoad.GetTrackName()+" to loadedTracks ", PrintStream.MEDIA_LOAD);
		}
	}

	public void UnloadPreviousTrack(){
		if (trackIndex > 0) {
			UnloadTrack(tracklist.entries [trackIndex - 1].GetTrack ());
		}
	}
	// this wasn't public until the weird video stuff happened.
	public void UnloadTrack(ITrack toUnload){
		if (preserveTrackLoads.Contains (toUnload)) {
			Diglbug.Log ("Preserved track " + toUnload.GetTrackName () + " not unloaded", PrintStream.AUDIO_GENERAL);
		} else {
			toUnload.Unload ();
			if (loadedTracks.Contains (toUnload)) {
				loadedTracks.Remove (toUnload);
			} else {
				Diglbug.Log ("Request to Unload was not contained in loadedTracks list "+toUnload.GetTrackName(), PrintStream.MEDIA_LOAD);
			}
		}
	}

	public void UnloadAllTracksExcept(ITrack protectedTrack){
		for (int i = loadedTracks.Count-1; i >= 0; i--) {
			if (loadedTracks[i] != protectedTrack) {
				UnloadTrack (loadedTracks [i]);
			} else {
				Diglbug.Log ("Protected track " + protectedTrack.GetTrackName() + " from UnloadAllTracks");
			}
		}
	}

	public void PlayTrackEntry(TracklistEntry entry){
		PlayTrackEntry (entry, 0f);
	}

	// public for rehearsal controls scrub.
	public void PlayTrackEntry(TracklistEntry entry, float timeSkip){
		int requestedIndex = IndexOfEntryInTracklist (entry);
		if (requestedIndex != -1) {
			if (!IsExpectedIndex (requestedIndex)) {
				Diglbug.Log ("Detected unorthodox track request ("+requestedIndex+") - unloading previously loaded tracks", PrintStream.MEDIA_LOAD);
				UnloadAllTracksExcept (entry.GetTrack());
			}
			PlayTrackEntryAtIndex (requestedIndex, timeSkip);
		} else {
			Diglbug.LogError ("Requested play of TrackEntry failed - entry not initialised in the Tracklist player's Tracklist");
		}
	}

	private bool IsExpectedIndex(int index){
		return (index == trackIndex || index == trackIndex + 1);
	}

	public void SkipToNextTrack(){
		Diglbug.Log ("Skipping track... ", PrintStream.AUDIO_PLAYBACK);
		bool shouldSkip = (currentOutput.GetTrack () is LoopingTrack);
		currentOutput.Skipped (); // just used for FireRemainingEvents.
		if (shouldSkip) {
			PlayNextTrack();
		}
		// this is firing the event to begin the next track. Looping don't, though.
	}

	public void PlayNextTrack(){
		PlayTrackEntryAtIndex (trackIndex+1);
	}

	public void PlayTrackEntryAtIndex(int index){
		PlayTrackEntryAtIndex (index, 0f);
	}

	private void PlayTrackEntryAtIndex(int index, float timeSkip){
		if (index < tracklist.entries.Length) {
			trackIndex = index;
			TracklistEntry entry = tracklist.GetTrackEntryAtIndex (index);
			HandlePlayRequest (entry, timeSkip);
		} else {
			Diglbug.Log ("Refused to play track at invalid index: " + index);
		}
	}

	private void HandlePlayRequest(TracklistEntry entry, float timeSkip){
		float fadeTime = entry.GetEntranceFadeTime ();
		ITrack nextTrack = entry.GetTrack ();

		currentOutput.FadeOut (fadeTime);
		// This even fires as the track is fading out, not strictly as it ends. Good enough for our purposes.
//		if (TrackEndsEvent != null) {
//			TrackEndsEvent (currentOutput.GetTrack ());
//		}
		currentOutput = GetOutputForNewEntry (entry);
		currentOutput.SetTrack (nextTrack);

		// safety
		LoadTrackIfNeeded (nextTrack);
		currentOutput.FadeIn(fadeTime);
		if (NewTrackBeginsEvent != null) {
			NewTrackBeginsEvent (nextTrack);
		}
		if (timeSkip != 0) {
			Diglbug.Log ("TracklistPlayer Skipping to catch up with signal delay: " + timeSkip, PrintStream.AUDIO_PLAYBACK);
			currentOutput.SetTrackTime (timeSkip);
		} else {
			Diglbug.Log ("TracklistPlayer timeSkip neglibile", PrintStream.AUDIO_PLAYBACK);
		}
	}

	public void SetWaitingTrackEntry(ITrack track){
		LoadTrackIfNeeded (track);
		SetTrack (track);
		Stop ();
	}

	private TrackOutput GetOutputForNewEntry(TracklistEntry entry){
		if (entry is VideoTracklistEntry) {
			return videoSystem;
		} else {
			multiPlayer.SwitchTracks ();
			return multiPlayer;
		}
	}

	private int IndexOfEntryInTracklist(TracklistEntry entry){
		for (int k = 0; k < tracklist.entries.Length; k++) {
			if (entry == tracklist.entries [k]) {
				return k;
			}
		}
		return -1;
	}

	private void LoopCurrentTrack(){
		LoopingTracklistEntry looingEntry = (LoopingTracklistEntry) tracklist.GetTrackEntryAtIndex (trackIndex);
		looingEntry.SwitchTracks();
		PlayTrackEntry (looingEntry);
	}

}


public struct ActSignalBundle{
	public Signal signal;
	public Act act;
	public ActSignalBundle(Act act, Signal signal){
		this.act = act;
		this.signal = signal;
	}
}

public struct ActBoolBundle{
	public Act act;
	public bool b;
	public ActBoolBundle(Act act, bool b){
		this.act = act;
		this.b = b;
	}
}