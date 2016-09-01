using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

/// <summary>
/// Used to play a playlist through a variety of TrackOutputs
/// </summary>
public class TracklistPlayer : WrappedTrackOutput{
	
	public MultiTrackOutput multiPlayer;

	public VideoPlaybackSystem videoPlayer;

	private TrackOutput currentOutput;

	public TrackUIControls display;

	public Tracklist trackList;
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
		

		TracklistEntry[] entries = trackList.entries;
		TracklistEntry entry;
		TracklistEntry nextEntry;
		EventTrack eventTrack;
		for(int k = 0; k<entries.Length-1; k++){ // so, we don't assign an event to the last track.
			entry = entries [k];
			nextEntry = entries [k + 1];
			eventTrack = (EventTrack)entry.GetTrack ();
			if (entry.Looping()) {
				eventTrack.AddStateEventAtTimeRemaining (LoopCurrentTrack, entry.GetTrack ().FadeTime ());
			} else {
				eventTrack.AddStateEventAtTimeRemaining (PlayNextTrack, nextEntry.GetTrack ().FadeTime ());
			}
			eventTrack.AddStateEventAtTime (UnloadPreviousTrack, 3f); // Let's Unload at 3...
			eventTrack.AddStateEventAtTime (LoadNextTrack, 6f); // and LoadNext at 6. Should be fine. For now.
		}
		#if !UNITY_EDITOR
		((MobileVideoPlayer) videoPlayer.GetPlayer()).InitialiseMobileVideoTracksInList(trackList);
		#endif

		// This is a poor way to make sure some variables get initialised so we can use our controls logically off the bat.
		PlayTrackAtIndex (0);
		display.Pause ();
	}

	public void LoadNextTrack(){
		if (trackIndex < trackList.entries.Length - 1) {
			trackList.entries [trackIndex + 1].GetTrack ().Load ();
		}
	}

	public void UnloadPreviousTrack(){
		if (trackIndex > 0) {
			trackList.entries [trackIndex + 1].GetTrack ().Unload ();
		}
	}

	public void PlayNextTrack(){
		trackIndex++;
		PlayTrackAtIndex (trackIndex);
		DisplayNextTrack ();
	}

	public void LoopCurrentTrack(){
//		LoopingTrack 
		((LoopingTracklistEntry) trackList.GetTrackEntryAtIndex (trackIndex)).SwitchTracks();
		PlayTrackAtIndex (trackIndex);
		DisplayNextTrack ();
	}

	public void PlayTracklistFromIndex(Tracklist list, int index){
		trackList = list;
		PlayTrackAtIndex (index);
	}

	public void PlayTrackAtIndex(int i){
		trackIndex = i;
		PlayTrackEntry(trackList.GetTrackEntryAtIndex (trackIndex));
	}

	public void PlayTrackEntry(TracklistEntry trackEntry){
		display.SetLoopingNote (trackEntry.Looping());
		HandlePlayRequest (trackEntry);
	}

	public void DisplayNextTrack(){
		SetNextTrackDisplay (trackIndex + 1);
	}

	public void SetNextTrackDisplay(int nextIndex){
		TracklistEntry upcomingTracklistEntry = trackList.GetTrackEntryAtIndex (nextIndex);
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

}
