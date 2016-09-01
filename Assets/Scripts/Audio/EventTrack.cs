using UnityEngine;
using System.Collections.Generic;

public abstract class EventTrack : AbstractTrack{
	
	public delegate void TrackEventDelegate ();
	private List<TimingEvent> events = new List<TimingEvent>();
	private float previousSourceTime;

	public virtual void AddEventAtTime(TrackEventDelegate newEvent, float occurOnTime){
		if(occurOnTime > GetTrackLength() || occurOnTime < 0f){
			Diglbug.LogError("Added an event with an invalid occurance time: "+occurOnTime+", (bounds: 0-"+GetTrackLength()+")");
		}
//		Diglbug.Log ("Added EventAtTime for EventTrack " + name, PrintStream.DELEGATES);
		events.Add (new TimingEvent(newEvent, occurOnTime));
	}

	public virtual void AddEventAtTimeRemaining(TrackEventDelegate newEvent, float occurOnTimeRemaining){
//		Diglbug.Log ("Added EventAtTimeRemaining for EventTrack " + name, PrintStream.DELEGATES);
		events.Add (new TimingEvent(newEvent, GetTrackLength()-occurOnTimeRemaining));
	}

	/// <summary>
	/// Used for manually setting the elapsed time, e.g. timeline scrubbing.
	/// Will avoid calling events under these circumstances.
	/// </summary>
	public void SetTimeElapsed(float timeElapsed){
		previousSourceTime = timeElapsed;
	}

	/// <summary>
	/// Set per-tick, to calculate whether an event's time has been crossed
	/// </summary>
	public void UpdateTimeElapsed (float timeElapsed){
		for (int i = events.Count - 1; i >= 0; i--) {
			if (events[i].time > previousSourceTime && events [i].time <= timeElapsed) {
				Diglbug.Log ("Event fired for " + name + " at time " + timeElapsed, PrintStream.DELEGATES);
				Diglbug.Log ("Event fire data: timeElapsed: " + timeElapsed + ", prev: " + previousSourceTime + ", time: " + events [i].time);
				events [i].function ();
			}
		}
		previousSourceTime = timeElapsed;
	}

}
