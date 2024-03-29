﻿using System;
using System.Collections;
using UnityEngine;

public class ShowAct : Act{

	public EventTracklistEntry expectingPayloadFrom;
	public float expectedTime = 5f;
	public bool isTimeFromEnd = true;

	private float expectedPayoadProgress;

	private float totalTimeUntilExpected = 0f;

	private bool delegatesAdded = false;

	protected override void Start(){
		base.Start ();
		StartCoroutine (RunDelegatesAfterFrame ());
	}

	private IEnumerator RunDelegatesAfterFrame(){
		yield return new WaitForEndOfFrame ();
		if (!delegatesAdded) {
			AddDelegates ();
			delegatesAdded = true;
		}
	}

	// TODO: Shift these delegates somewhere much better...
	public override void ActChangedTo(){
		if (!delegatesAdded) {
			AddDelegates ();
			delegatesAdded = true;
		}
		BLE.Instance.Manager.ClearExpectedPayload ();
		BLE.Instance.Manager.ClearUpcomingPayload ();
		BLE.Instance.Manager.SetUpcomingPayload (exitPayload);
		SetExpectedPayloadProgress ();

		RecalculateTotalTimeUntilExpected ();
	}

	private void AddDelegates(){
		bool addToRest = false;
		for (int k = 0; k < trackEntries.Length; k++) {
			if (addToRest) { // this ensures if we skip to a track beyond the defined one, we still proc.
				trackEntries[k].AddStateEventAtTime (ExpectedTimeReached, 0.5f);
			}
			if (trackEntries [k] == expectingPayloadFrom) {
				if (isTimeFromEnd) {
					trackEntries [k].AddStateEventAtTimeRemaining (ExpectedTimeReached, expectedTime);
				} else {
					trackEntries [k].AddStateEventAtTime (ExpectedTimeReached, expectedTime);
				}
				addToRest = true;
			}
		}
	}

	private void RecalculateTotalTimeUntilExpected(){
		TracklistEntry te;
		totalTimeUntilExpected = 0f;
		for (int k = 0; k < trackEntries.Length; k++) {
			te = trackEntries [k];
			if (te == expectingPayloadFrom) {
				totalTimeUntilExpected += (isTimeFromEnd ? te.GetTrackLength () - expectedTime : expectedTime);
				break;
			} else {
				totalTimeUntilExpected += te.GetTrackLength ();
			}
		}
	}

	private void ExpectedTimeReached(){
		BLE.Instance.Manager.PayloadExpected (exitPayload);
	}

	public float GetTimeUntilExpected(){
		return Mathf.Max (totalTimeUntilExpected - GetActTimeElapsed (), 0f);
	}

	private void SetExpectedPayloadProgress(){
		ITrack t = expectingPayloadFrom.GetTrack ();
		int index = IndexOfTrack (t);
		if (index == -1) {
			Diglbug.LogError ("Cannot SetExpectedPayloadProgress from a track not defined in this Act " + name);
		} else {
			float c;
			if (isTimeFromEnd) {
				c = GetTotalProgressBeforeIndex (index+1);
				c -= (t.GetTrackLength () - expectedTime) * t.GetInverseTrackLength ();
			} else {
				c = GetTotalProgressBeforeIndex (index + 1);
				c += expectedTime * t.GetInverseTrackLength ();
			}
			expectedPayoadProgress = c;
		}
	}

	public float GetExpectedPayloadProgress(){
		return expectedPayoadProgress;
	}

}