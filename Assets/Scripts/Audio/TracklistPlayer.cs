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
		EventTrack eventTrack;
		foreach (TracklistEntry entry in entries) {
			if (entry.Looping()) {
				eventTrack = (EventTrack)entry.GetTrack ();
				eventTrack.AddEventAtTimeRemaining (LoopCurrentTrack, entry.GetTrack ().FadeTime ());
			} else {
				eventTrack = (EventTrack)entry.GetTrack ();
				eventTrack.AddEventAtTimeRemaining (PlayNextTrack, entry.GetTrack ().FadeTime ());
			}
		}

		// This is a poor way to make sure some variables get initialised so we can use our controls logically off the bat.
		PlayTrackAtIndex (0);
		display.Pause ();
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
