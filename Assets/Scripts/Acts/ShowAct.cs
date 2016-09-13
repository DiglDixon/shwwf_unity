using System;
using UnityEngine;

public class ShowAct : Act{

	public EventTracklistEntry expectingPayloadFrom;
	public float expectedTime = 5f;
	public bool isTimeFromEnd = true;

	private float expectedPayoadProgress;
	private float inverse_expectedPayloadProgress;

	private float totalTimeUntilExpected = 0f;

	// TODO: Shift these delegates somewhere much better...
	public override void ActChangedTo(){
		BLE.Instance.Manager.ClearExpectedPayload ();
		BLE.Instance.Manager.SetUpcomingPayload (exitPayload);
		SetExpectedPayloadProgress ();
		if (isTimeFromEnd) {
			expectingPayloadFrom.AddStateEventAtTimeRemaining (ExpectedTimeReached, expectedTime);
		} else {
			expectingPayloadFrom.AddStateEventAtTime (ExpectedTimeReached, expectedTime);
		}

		RecalculateTotalTimeUntilExpected ();


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
			inverse_expectedPayloadProgress = 1f / expectedPayoadProgress;
		}
	}

	public float GetExpectedPayloadProgress(){
		return expectedPayoadProgress;
	}

}