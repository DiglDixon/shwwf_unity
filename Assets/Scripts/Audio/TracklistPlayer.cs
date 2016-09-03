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
		PlayTrackEntry (tracklist.entries[0]);
		display.Pause ();
	}

	private void SetTracklist(Tracklist t){
		this.tracklist = t;
		TracklistEntry[] entries = tracklist.entries;
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
		((MobileVideoPlayer) videoPlayer.GetPlayer()).InitialiseMobileVideoTracksInList(tracklist);
		#endif
	}

	public void LoadNextTrack(){
		if (trackIndex < tracklist.entries.Length - 1) {
			tracklist.entries [trackIndex + 1].GetTrack ().Load ();
		}
	}

	public void UnloadPreviousTrack(){
		if (trackIndex > 0) {
			tracklist.entries [trackIndex - 1].GetTrack ().Unload ();
		}
	}

	public void PlayTrackEntry(TracklistEntry entry){
		int requestedIndex = IndexOfEntryInTracklist (entry);
		if (requestedIndex != -1) {
			PlayTrackEntryAtIndex (requestedIndex);
		} else {
			Diglbug.LogError ("Requested play of TrackEntry failed - entry not initialised in the Tracklist player's Tracklist");
		}
	}

	public void PlayNextTrack(){
		PlayTrackEntryAtIndex (trackIndex+1);
	}

	public void PlayTrackEntryAtIndex(int index){
		trackIndex = index;
		TracklistEntry entry = tracklist.GetTrackEntryAtIndex (index);
		display.SetLoopingNote (entry.Looping());
		SetNextTrackDisplay (trackIndex+1);
		HandlePlayRequest (entry);
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
