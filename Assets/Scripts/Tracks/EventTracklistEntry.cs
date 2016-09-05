using UnityEngine;
[RequireComponent (typeof(EventTrack))]
public abstract class EventTracklistEntry : TracklistEntry{

	public override void Initialise(){
		((EventTrack)GetTrack()).GatherEventsFromChildren ();
	}

	// These is a bit crude. Meant as a way to allow changing all nested tracks
	// overrided, for example, in LoopingTracklistEntry.
	public virtual void AddStateEventAtTimeRemaining(EventTrack.TrackEventDelegate newEvent, float eventTimeRemaining){
		((EventTrack)GetTrack ()).AddStateEventAtTimeRemaining (newEvent, eventTimeRemaining);
	}

	public virtual void AddStateEventAtTime(EventTrack.TrackEventDelegate newEvent, float eventTime){
		((EventTrack)GetTrack ()).AddStateEventAtTime (newEvent, eventTime);
	}

	public virtual void AddEventAtTimeRemaining(EventTrack.TrackEventDelegate newEvent, float eventTimeRemaining){
		((EventTrack)GetTrack ()).AddEventAtTimeRemaining (newEvent, eventTimeRemaining);
	}

	public virtual void AddEventAtTime(EventTrack.TrackEventDelegate newEvent, float eventTime){
		((EventTrack)GetTrack ()).AddEventAtTime (newEvent, eventTime);
	}

}