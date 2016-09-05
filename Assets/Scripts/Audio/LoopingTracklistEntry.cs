using UnityEngine;

[RequireComponent (typeof(LoopingTrack))]
public class LoopingTracklistEntry : AudioTracklistEntry{

	private LoopingTrack cloneTrack;
	private bool usingClone = false;

	public void SetCloneTrack(LoopingTrack clone){
		cloneTrack = clone;
	}

	public override void FetchTrack(){
		base.FetchTrack ();
	}

	public override void Initialise(){
		// Order is important here to stop gathering the clone's children.
		// gather children
		GetComponent<EventTrack> ().GatherEventsFromChildren ();
		// clone
		((LoopingTrack) track).Clone ();
		// make the clone gather children.
		cloneTrack.GatherEventsFromChildren ();
	}

	public void SwitchTracks(){
		usingClone = !usingClone;
	}

	public override ITrack GetTrack(){
		return usingClone ? cloneTrack : track;
	}

	// These is a bit crude. Meant as a way to allow changing all nested tracks
	// overrided, for example, in LoopingTracklistEntry.
	public override void AddStateEventAtTimeRemaining(EventTrack.TrackEventDelegate newEvent, float eventTimeRemaining){
		((EventTrack)GetTrack ()).AddStateEventAtTimeRemaining (newEvent, eventTimeRemaining);
		cloneTrack.AddStateEventAtTimeRemaining (newEvent, eventTimeRemaining);
	}

	public override void AddStateEventAtTime(EventTrack.TrackEventDelegate newEvent, float eventTime){
		((EventTrack)GetTrack ()).AddStateEventAtTime (newEvent, eventTime);
		cloneTrack.AddStateEventAtTime (newEvent, eventTime);
	}


	public override void AddEventAtTimeRemaining(EventTrack.TrackEventDelegate newEvent, float eventTimeRemaining){
		((EventTrack)GetTrack ()).AddEventAtTimeRemaining (newEvent, eventTimeRemaining);
		cloneTrack.AddEventAtTimeRemaining (newEvent, eventTimeRemaining);
	}

	public override void AddEventAtTime(EventTrack.TrackEventDelegate newEvent, float eventTime){
		((EventTrack)GetTrack ()).AddEventAtTime (newEvent, eventTime);
		cloneTrack.AddEventAtTime (newEvent, eventTime);
	}

}

