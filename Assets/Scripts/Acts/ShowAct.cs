using System;

public class ShowAct : Act{

	private float expectedPayoadProgress;
	private float inverse_expectedPayloadProgress;
	public EventTracklistEntry expectingPayloadFrom;
	public float expectedTime = 5f;
	public bool isTimeFromEnd = true;


	public override void ActChangedTo(){

		BLE.Instance.Manager.SetUpcomingPayload (exitPayload);

		SetExpectedPayloadProgress ();
		if (isTimeFromEnd) {
			expectingPayloadFrom.AddStateEventAtTimeRemaining (ExpectedTimeReached, expectedTime);
		} else {
			expectingPayloadFrom.AddStateEventAtTime (ExpectedTimeReached, expectedTime);
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

	public float GetExpectedPayloadProgress(){
		return expectedPayoadProgress;
	}

}