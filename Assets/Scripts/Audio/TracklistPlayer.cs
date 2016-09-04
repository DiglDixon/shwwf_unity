using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

/// <summary>
/// Used to play a playlist through a variety of TrackOutputs
/// </summary>
public class TracklistPlayer : WrappedTrackOutput{
	
	public MultiTrackOutput multiPlayer;

	public VideoPlaybackSystem videoPlayer;

	private TrackOutput currentOutput;

	public TrackUIControls display;

	private List<ITrack> loadedTracks = new List<ITrack> ();

	public Tracklist tracklist;
	private int trackIndex = 0;

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

		// This is a poor way to make sure some variables get initialised so we can use our controls logically off the bat.
		LoadTrack(tracklist.entries[0].GetTrack());
		PlayTrackEntry (tracklist.entries[0]);

		display.Pause ();
	}

	private void SetTracklist(Tracklist t){
		this.tracklist = t;
		TracklistEntry[] entries = tracklist.entries;
		EventTracklistEntry entry;
		TracklistEntry nextEntry;
		for(int k = 0; k<entries.Length; k++){
			entry = (EventTracklistEntry) entries [k];
			if (entry is LoopingTracklistEntry) {
				entry.AddStateEventAtTimeRemaining (LoopCurrentTrack, entry.GetTrack ().FadeTime ());
			} else if (k < entries.Length-1){
				nextEntry = entries [k + 1];
				entry.AddStateEventAtTimeRemaining (PlayNextTrack, nextEntry.GetTrack ().FadeTime ());
			}
			entry.AddStateEventAtTime (UnloadPreviousTrack, 2f); // Let's Unload at 3...
			entry.AddStateEventAtTime (LoadNextTrack, 4f); // and LoadNext at 6. Should be fine. For now.
		}
		#if !UNITY_EDITOR
		((MobileVideoPlayer) videoPlayer.GetPlayer()).InitialiseMobileVideoTracksInList(tracklist);
		#endif
	}

	public void LoadNextTrack(){
		if (trackIndex < tracklist.entries.Length - 1) {
			LoadTrack(tracklist.entries [trackIndex + 1].GetTrack ());
		}
	}

	private void LoadTrack(ITrack toLoad){
		toLoad.Load ();
		loadedTracks.Add (toLoad);
		Diglbug.Log ("Added track to loadedTracks "+toLoad.GetTrackName(), PrintStream.AUDIO_LOAD);
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
			Diglbug.Log ("Request to Unload was not contained in loadedTracks list "+toUnload.GetTrackName(), PrintStream.AUDIO_LOAD);
		}
	}

	public void UnloadAllTracks(){
		for (int i = loadedTracks.Count-1; i >= 0; i--) {
			UnloadTrack (loadedTracks [i]);
		}
	}

	public void PlayTrackEntry(TracklistEntry entry){
		int requestedIndex = IndexOfEntryInTracklist (entry);
		if (requestedIndex != -1) {
			if (IsExpectedIndex (requestedIndex)) {
				PlayTrackEntryAtIndex (requestedIndex);
			} else {
				Diglbug.Log ("Detected unorthodox track request - unloading previously loaded tracks", PrintStream.AUDIO_LOAD);
				UnloadAllTracks ();
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
			display.SetLoopingNote (entry);
			SetNextTrackDisplay (trackIndex + 1);
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

	public void SetNextTrackDisplay(int index){
		TracklistEntry upcomingTracklistEntry = tracklist.GetTrackEntryAtIndex (index);
		display.SetUpcomingTrackDisplay(upcomingTracklistEntry);
	}

	private void HandlePlayRequest(TracklistEntry entry){
		float fadeTime = entry.GetEntranceFadeTime ();

		currentOutput.FadeOut (fadeTime);
		TrackOutput nextOutput = null;
		if (entry is VideoTracklistEntry) {
			nextOutput = videoPlayer;
		} else {
			nextOutput = multiPlayer;
			multiPlayer.SwitchTracks ();
		}
		currentOutput = nextOutput;
		currentOutput.SetTrack (entry.GetTrack());
		currentOutput.FadeIn(fadeTime);

		display.ChangeTrackData (multiPlayer);
	}

	private void LoopCurrentTrack(){
		LoopingTracklistEntry looingEntry = (LoopingTracklistEntry) tracklist.GetTrackEntryAtIndex (trackIndex);
		looingEntry.SwitchTracks();
		PlayTrackEntry (looingEntry);
	}

}
