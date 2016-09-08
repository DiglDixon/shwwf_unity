using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

/// <summary>
/// Used to play a playlist through a variety of TrackOutputs
/// </summary>
public class TracklistPlayer : WrappedTrackOutput{
	
	public MultiTrackOutput multiPlayer;

	public VideoPlaybackSystem videoSystem;

	private TrackOutput currentOutput;

	private List<ITrack> loadedTracks = new List<ITrack> ();

	public Tracklist tracklist;
	private int trackIndex = 0;

	public delegate void NewTrackBeginsDelegate (ITrack track);
	public event NewTrackBeginsDelegate NewTrackBeginsEvent;

	public delegate void TrackEndsDelegate (ITrack track);
	public event TrackEndsDelegate TrackEndsEvent;

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
//		PrepareTrack(tracklist.entries[0]);
	}

	public void PrepareTrack(TracklistEntry entry){
		LoadTrack(entry.GetTrack());
		PlayTrackEntry (entry);
		Pause ();
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
			entry.AddStateEventAtTime (UnloadPreviousTrack, 4f); // Let's Unload at 3...
			entry.AddStateEventAtTime (LoadNextTrack, 5f); // and LoadNext at 6. Should be fine. For now.
		}
		#if !UNITY_EDITOR
		((MobileVideoPlayer) videoSystem.GetPlayer()).InitialiseMobileVideoTracksInList(tracklist);
		#endif
	}

	public void LoadNextTrack(int ahead){
		if (trackIndex < tracklist.entries.Length - 1) {
			LoadTrack(GetNextTrack(ahead));
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

	private void LoadTrack(ITrack toLoad){
		toLoad.Load ();
		loadedTracks.Add (toLoad);
		Diglbug.Log ("Added track to loadedTracks "+toLoad.GetTrackName(), PrintStream.MEDIA_LOAD);
	}

	public void UnloadPreviousTrack(){
		if (trackIndex > 0) {
			UnloadTrack(tracklist.entries [trackIndex - 1].GetTrack ());
		}
	}

	private void UnloadTrack(ITrack toUnload){
		toUnload.Unload ();
		if (loadedTracks.Contains (toUnload)) {
			loadedTracks.Remove (toUnload);
		} else {
			Diglbug.Log ("Request to Unload was not contained in loadedTracks list "+toUnload.GetTrackName(), PrintStream.MEDIA_LOAD);
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
		int requestedIndex = IndexOfEntryInTracklist (entry);
		if (requestedIndex != -1) {
			if (IsExpectedIndex (requestedIndex)) {
				PlayTrackEntryAtIndex (requestedIndex);
			} else {
				Diglbug.Log ("Detected unorthodox track request ("+requestedIndex+") - unloading previously loaded tracks", PrintStream.MEDIA_LOAD);
				UnloadAllTracksExcept (entry.GetTrack());
				PlayTrackEntryAtIndex (requestedIndex);
			}
		} else {
			Diglbug.LogError ("Requested play of TrackEntry failed - entry not initialised in the Tracklist player's Tracklist");
		}
	}

	private bool IsExpectedIndex(int index){
		return (index == trackIndex || index == trackIndex + 1);
	}

	public void PlayNextTrack(){
		PlayTrackEntryAtIndex (trackIndex+1);
	}

	public void PlayTrackEntryAtIndex(int index){
		if (index < tracklist.entries.Length) {
			trackIndex = index;
			TracklistEntry entry = tracklist.GetTrackEntryAtIndex (index);
			HandlePlayRequest (entry);
		} else {
			Diglbug.Log ("Refused to play track at invalid index: " + index);
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

	private void HandlePlayRequest(TracklistEntry entry){
		float fadeTime = entry.GetEntranceFadeTime ();

		currentOutput.FadeOut (fadeTime);
		TrackOutput nextOutput = null;
		if (entry is VideoTracklistEntry) {
			nextOutput = videoSystem;
		} else {
			nextOutput = multiPlayer;
			multiPlayer.SwitchTracks ();
		}
		if (entry is LoopingTracklistEntry) {
			// don't clear the expected payload
		} else {
			BLE.Instance.Manager.ClearExpectedPayload ();
		}
		// This even fires as the track is fading out, not strictly as it ends. Good enough for our purposes.
		if (TrackEndsEvent != null) {
			TrackEndsEvent (currentOutput.GetTrack ());
		}
		currentOutput = nextOutput;
		currentOutput.SetTrack (entry.GetTrack());
		currentOutput.FadeIn(fadeTime);
		if (NewTrackBeginsEvent != null) {
			NewTrackBeginsEvent (entry.GetTrack ());
		}
	}

	private void LoopCurrentTrack(){
		LoopingTracklistEntry looingEntry = (LoopingTracklistEntry) tracklist.GetTrackEntryAtIndex (trackIndex);
		looingEntry.SwitchTracks();
		PlayTrackEntry (looingEntry);
	}

}
