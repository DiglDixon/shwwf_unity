using UnityEngine;
using System.Collections.Generic;

public abstract class EventTrack : AbstractTrack{
	
	public delegate void TrackEventDelegate ();
	private List<TimingEvent> stateEvents = new List<TimingEvent>();
	private List<TimingEvent> momentaryEvents = new List<TimingEvent>();
	private List<RegionTimingEvent> regionEvents = new List<RegionTimingEvent>();
	private float previousSourceTime;

//	private bool eventsEnabled = false;

	#if UNITY_EDITOR
	public bool updateName = false;
	#endif

	public void Reset(){
		previousSourceTime = 0f;
	}

	public void GatherEventsFromChildren(){
		CustomTrackTimeEvent[] childEvents = GetComponentsInChildren<CustomTrackTimeEvent> ();
		CustomRegionTrackTimeEvent regionEvent;
		Diglbug.Log (name + " adding " + childEvents.Length + " child delegate events", PrintStream.DELEGATES);
		for (int k = 0; k < childEvents.Length; k++) {
			if (childEvents [k] is CustomRegionTrackTimeEvent) {
				regionEvent = (CustomRegionTrackTimeEvent)childEvents [k];
				AddRegionEvent (regionEvent);
			} else {
				
				if (childEvents [k].isStateEvent) {
					if (childEvents [k].occurAtTimeFromEnd) {
						AddStateEventAtTimeRemaining (childEvents [k].CustomEvent, childEvents [k].occurAtTime);
					} else {
						AddStateEventAtTime (childEvents [k].CustomEvent, childEvents [k].occurAtTime);
					}
				} else {
					if (childEvents [k].occurAtTimeFromEnd) {
						AddEventAtTimeRemaining (childEvents [k].CustomEvent, childEvents [k].occurAtTime);
					} else {
						AddEventAtTime (childEvents [k].CustomEvent, childEvents [k].occurAtTime);
					}
				}

			}
		}
	}

	public void AddStateEventAtTimeRemaining(TrackEventDelegate newEvent, float eventTimeRemaining){
		float eventTime = GetTrackLength () - eventTimeRemaining;
		AddStateEventAtTime (newEvent, eventTime);
	}

	public virtual void AddStateEventAtTime(TrackEventDelegate newEvent, float eventTime){
		AddEventToList(stateEvents, newEvent, eventTime);
	}


	public void AddEventAtTimeRemaining(TrackEventDelegate newEvent, float eventTimeRemaining){
		float eventTime = GetTrackLength () - eventTimeRemaining;
		AddEventAtTime (newEvent, eventTime);
	}

	public virtual void AddEventAtTime(TrackEventDelegate newEvent, float eventTime){
		AddEventToList(momentaryEvents, newEvent, eventTime);
	}

	private void AddRegionEvent(CustomRegionTrackTimeEvent e){
		regionEvents.Add(new RegionTimingEvent(e));
	}

	private void AddEventToList(List<TimingEvent> list, TrackEventDelegate newEvent, float eventTime){
		if (IsValidEventTime (eventTime)) {
			list.Add (new TimingEvent(newEvent, eventTime));
		} else {
			InvalidEventTimeGiven (eventTime);
		}
	}

	private bool IsValidEventTime(float time){
		return time < GetTrackLength () || time > 0f;
	}

	private void InvalidEventTimeGiven(float eventTime){
		Diglbug.LogError ("Added an event with an invalid occurance time: " + eventTime + ", (bounds: 0-" + GetTrackLength () + ")");
	}

	/// <summary>
	/// Used for manually setting the elapsed time, e.g. timeline scrubbing.
	/// Will avoid calling events under these circumstances.
	/// </summary>
	public void SetTimeElapsed(float timeElapsed){
		CheckEvents (stateEvents, previousSourceTime, timeElapsed);

		if (timeElapsed < previousSourceTime) { // If we've skipped backwards, we need to hit all the regions from the start
			CheckRegionEvents (regionEvents, 0f, timeElapsed);
		} else { // Otherwise, we're cool.
			CheckRegionEvents (regionEvents, previousSourceTime, timeElapsed);
		}
		previousSourceTime = timeElapsed;
	}

	/// <summary>
	/// Set per-tick, to calculate whether an event's time has been crossed
	/// </summary>
	public void UpdateTimeElapsed (float currentSourceTime){
		CheckEvents (momentaryEvents, previousSourceTime, currentSourceTime);
		CheckEvents (stateEvents, previousSourceTime, currentSourceTime);
		CheckRegionEvents (regionEvents, previousSourceTime, currentSourceTime);
		previousSourceTime = currentSourceTime;
	}

	private void CheckEvents(List<TimingEvent> events, float previousTime, float currentTime){
		
		for (int k = 0; k<events.Count; k++) {
			if (EventShouldOccur(events[k], previousSourceTime, currentTime)) {
				Diglbug.Log (name.Substring(0, 5)+"(def:"+events[k].time+")"+" Event fired for at time " + currentTime, PrintStream.DELEGATES);
				events [k].function ();
			}
		}

	}

	private void CheckRegionEvents(List<RegionTimingEvent> events, float previousTime, float currentTime){
		float startTime;
		float endTime;
		for (int k = 0; k<events.Count; k++) {
			startTime = events [k].startTime;
			endTime = events [k].endTime;
			if (endTime < startTime) {
				// this will be a Pair, which are deppd for now. endTime can't be lt startTime.
				continue;
			}

			if (previousTime < startTime && currentTime > endTime) {
				Diglbug.Log (events [k].eventName + " SKIP (def:"+startTime+"->"+endTime+") at time " + currentTime, PrintStream.DELEGATES);
				events [k].skipFunction ();
			} else if (previousTime > startTime && previousTime < endTime && currentTime > endTime) {
				Diglbug.Log (events [k].eventName + " END (def:"+startTime+"->"+endTime+") at time " + currentTime, PrintStream.DELEGATES);
				events [k].endFunction ();
			}else if (previousTime < events [k].startTime && currentTime > events [k].startTime) {
				Diglbug.Log (events [k].eventName + " START (def:"+startTime+"->"+endTime+") at time " + currentTime, PrintStream.DELEGATES);
				events [k].startFunction ();
			}
		}
	}

	private bool EventShouldOccur(TimingEvent e, float previousTime, float currentTime){
		return (e.time > previousSourceTime) && (e.time <= currentTime);
	}

	#if UNITY_EDITOR
	public void UpdateName(){
		updateName = false;
		CustomTrackTimeEvent[] customEvents = GetComponentsInChildren<CustomTrackTimeEvent> ();
		gameObject.name = "("+customEvents.Length+") "+GetTrackName ();
	}

	private void OnValidate(){
		UpdateName ();
	}
	#endif

}
