﻿using UnityEngine;
using System;
using System.Collections;

public abstract class Act : EnsureDefinedActChild{
	
	protected EventTracklistEntry[] trackEntries = new EventTracklistEntry[0];
	private float totalTrackLength;
	private float inverse_totalTrackLength;
	private float[] normalisedTrackLengths;

	public TracklistPlayer player;

	public DefinedAct definedAct;

	public Payload entryPayload;
	public Payload exitPayload;
	public bool autoAssignExit = true;


	protected virtual void Start(){
		
		trackEntries = new EventTracklistEntry[transform.childCount];
		for (int k = 0; k < transform.childCount; k++) {
			trackEntries [k] = transform.GetChild (k).GetComponent<EventTracklistEntry> ();
		}

		totalTrackLength = 0f;
		for (int k = 0; k < trackEntries.Length; k++) {
			totalTrackLength += trackEntries [k].GetTrackLength ();
		}
		inverse_totalTrackLength = 1f / totalTrackLength;

		normalisedTrackLengths = new float[trackEntries.Length];
		for (int k = 0; k < normalisedTrackLengths.Length; k++) {
			normalisedTrackLengths [k] = trackEntries [k].GetTrackLength () * inverse_totalTrackLength;
		}

	}

	public virtual void Initialise(){
		//
	}

	public virtual void Begin(){
		player.PlayTrackEntry (trackEntries [0]);
	}

	public virtual void ActChangedTo(){
		//
	}

	public float GetActTimeElapsed(){
		ITrack currentTrack = player.GetTrack ();
		float ret = 0f;
		for (int k = 0; k < trackEntries.Length; k++) {
			if (currentTrack == trackEntries [k].GetTrack ()) {
				ret += player.GetTimeElapsed ();
				break;
			} else {
				ret += trackEntries [k].GetTrackLength ();
			}
		}
		return ret;
	}

	public float GetProgress(){
		ITrack currentTrack = player.GetTrack ();
		for (int k = 0; k < trackEntries.Length; k++) {
			if(currentTrack == trackEntries[k].GetTrack()){
				return GetProgressFromEntryIndex (k);
			}
		}
		Diglbug.Log ("Warning: Called GetProgress from an Act that did not have a playing track", PrintStream.ACTS);
		return 0;
	}

	private float GetProgressFromEntryIndex(int i){
		float c = GetTotalProgressBeforeIndex (i);
		c += player.GetProgress () * normalisedTrackLengths[i];
		return c;
	}

	protected float GetTotalProgressBeforeIndex(int i){
		float c = 0f;
		for (int k = 0; k < i; k++) {
			c += normalisedTrackLengths [k];
		}
		return c;
	}

	public float GetLastTrackStartProgress(){
		return 1f - normalisedTrackLengths [normalisedTrackLengths.Length - 1];
	}

	public bool ContainsTrack(ITrack t){
		for (int k = 0; k < trackEntries.Length; k++) {
			if (trackEntries [k].GetTrack () == t) {
				return true;
			}
		}
		return false;
	}

	public int IndexOfTrack(ITrack t){
		for (int k = 0; k < trackEntries.Length; k++) {
			if (trackEntries [k].GetTrack () == t) {
				return k;
			}
		}
		return -1;
	}

	/* EnsureDefinedActChild mandatory overrides */

	public override void SetDefinedAct(DefinedAct a){
		definedAct = a;
	}

	public override DefinedAct GetDefinedAct(){
		return definedAct;
	}

	protected override string GetNameString (){
		TracklistEntry[] entries = GetComponentsInChildren<TracklistEntry> ();
		return definedAct.ToString ()+" ("+entries.Length+")";
	}

	/* Other */

	protected override void OnValidate(){
		base.OnValidate ();
		if (autoAssignExit) {
			if ((int)entryPayload == Enum.GetNames (typeof(Payload)).Length - 1) {
				exitPayload = Payload.NONE;
			} else {
				exitPayload = (Payload)(((int)entryPayload) + 1);
			}

		}
	}


}