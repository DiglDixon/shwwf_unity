﻿using UnityEngine;
using System.Collections.Generic;

public abstract class EventTrack : AbstractTrack{
	
	public delegate void TrackEventDelegate ();
	private List<TimingEvent> stateEvents = new List<TimingEvent>();
	private List<TimingEvent> momentaryEvents = new List<TimingEvent>();
	private float previousSourceTime;

//	private bool eventsEnabled = false;

	#if UNITY_EDITOR
	public bool updateName = false;
	#endif

	public void GatherEventsFromChildren(){
		CustomTrackTimeEvent[] childEvents = GetComponentsInChildren<CustomTrackTimeEvent> ();
		for (int k = 0; k < childEvents.Length; k++) {
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

//	public void EnableEvents(){
//		eventsEnabled = true;
//	}
//
//	public void DisableEvents(){
//		eventsEnabled = false;
//	}

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
//		Diglbug.Log ("SetTimeElapsed " + timeElapsed + " against "+ stateEvents.Count + " events " + GetTrackName());
		CheckEvents (stateEvents, previousSourceTime, timeElapsed);
		previousSourceTime = timeElapsed;
	}

	/// <summary>
	/// Set per-tick, to calculate whether an event's time has been crossed
	/// </summary>
	public void UpdateTimeElapsed (float currentSourceTime){
//		Diglbug.Log ("Checking " + currentSourceTime + " against " + momentaryEvents.Count + ", " + stateEvents.Count + " events " + GetTrackName());
//		Diglbug.Log ("Momentary: "+momentaryEvents.Count);
		CheckEvents (momentaryEvents, previousSourceTime, currentSourceTime);
//		Diglbug.Log ("State: " + stateEvents.Count);
		CheckEvents (stateEvents, previousSourceTime, currentSourceTime);
		previousSourceTime = currentSourceTime;
	}

	private void CheckEvents(List<TimingEvent> events, float previousTime, float currentTime){
		
		for (int k = 0; k<events.Count; k++) {
//			Diglbug.Log ("Iterating " + name + " at time " + currentTime+", prev:"+previousTime+" (def:"+events[i].time+")", PrintStream.DELEGATES);
			if (EventShouldOccur(events[k], previousSourceTime, currentTime)) {
				Diglbug.Log ("Event fired for " + name + " at time " + currentTime+" (def:"+events[k].time+")", PrintStream.DELEGATES);
				events [k].function ();
			}
		}

	}

	private bool EventShouldOccur(TimingEvent e, float previousTime, float currentTime){
//		if (!eventsEnabled)
//			return false;
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
