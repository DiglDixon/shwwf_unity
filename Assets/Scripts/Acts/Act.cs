using UnityEngine;
using System;
using System.Collections;

public class Act : EnsurePayloadChild{

	public string ActName;
	private EventTracklistEntry[] actEntries = new EventTracklistEntry[0];
	private float totalTrackLength;
	private float inverse_totalTrackLength;
	private float[] normalisedTrackLengths;

	public TracklistPlayer player;

	public Payload entryPayload;
	public Payload exitPayload;
	public bool autoAssignExit = true;

	private float expectedPayoadProgress;
	private float inverse_expectedPayloadProgress;
	public EventTracklistEntry expectingPayloadFrom;
	public float expectedTime = 5f;
	public bool isTimeFromEnd = true;

	private void Start(){
		actEntries = new EventTracklistEntry[transform.childCount];
		for (int k = 0; k < transform.childCount; k++) {
			actEntries [k] = transform.GetChild (k).GetComponent<EventTracklistEntry> ();
		}

		totalTrackLength = 0f;
		for (int k = 0; k < actEntries.Length; k++) {
			totalTrackLength += actEntries [k].GetTrackLength ();
		}
		inverse_totalTrackLength = 1f / totalTrackLength;

		normalisedTrackLengths = new float[actEntries.Length];
		for (int k = 0; k < normalisedTrackLengths.Length; k++) {
			normalisedTrackLengths [k] = actEntries [k].GetTrackLength () * inverse_totalTrackLength;
		}

	}

	private void ExpectedTimeReached(){
		BLE.Instance.Manager.PayloadExpected (exitPayload);
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

	public void Begin(){
		BLE.Instance.Manager.SetUpcomingPayload (exitPayload);
		SetExpectedPayloadProgress ();
		if (isTimeFromEnd) {
			expectingPayloadFrom.AddStateEventAtTimeRemaining (ExpectedTimeReached, expectedTime);
		} else {
			expectingPayloadFrom.AddStateEventAtTime (ExpectedTimeReached, expectedTime);
		}
	}

	public float GetProgress(){
		ITrack currentTrack = player.GetTrack ();
		for (int k = 0; k < actEntries.Length; k++) {
			if(currentTrack == actEntries[k].GetTrack()){
				return GetProgressFromEntryIndex (k);
			}
		}
		Diglbug.Log ("Warning: Called GetProgress from an Act that did not have a playing track", PrintStream.ACTS);
		return 0;
	}

//	public float GetProgressTowardsExpectedPayload(){
//		return GetProgress () / expectedPayoadProgress;
//	}

	private float GetProgressFromEntryIndex(int i){
		float c = GetTotalProgressBeforeIndex (i);
		c += player.GetProgress () * normalisedTrackLengths[i];
		return c;
	}

	private float GetTotalProgressBeforeIndex(int i){
		float c = 0f;
		for (int k = 0; k < i; k++) {
			c += normalisedTrackLengths [k];
		}
		return c;
	}

	public float GetExpectedPayloadProgress(){
		return expectedPayoadProgress;//1f - normalisedTrackLengths [normalisedTrackLengths.Length - 1];
	}

	public float GetProgressForLastTrack(){
		return 1f - normalisedTrackLengths [normalisedTrackLengths.Length - 1];
	}

	public bool ContainsTrack(ITrack t){
		for (int k = 0; k < actEntries.Length; k++) {
			if (actEntries [k].GetTrack () == t) {
				return true;
			}
		}
		return false;
	}

	public int IndexOfTrack(ITrack t){
		for (int k = 0; k < actEntries.Length; k++) {
			if (actEntries [k].GetTrack () == t) {
				return k;
			}
		}
		return -1;
	}

//	public void BeginAct(){
//		player.PlayTrackEntry (actEntries [0]);
//	}

	// EnsurePayloadChild overrides

	public override void SetPayload(Payload p){
		entryPayload = p;
	}

	public override Payload GetPayload(){
		return entryPayload;
	}

	protected override string GetNameString (){
		return "Act_" + entryPayload.ToString ();
	}

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