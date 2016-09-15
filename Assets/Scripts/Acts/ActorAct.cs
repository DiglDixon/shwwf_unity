

public class ActorAct : Act{
	// uhm.

	public EventTracklistEntry lastTrackWithActingContent;
	public float timeActingContentEnds = 3f;
	public bool isTimeFromEnd = true;

	private float actingLength;

	public delegate void ActingContentCompleteDelegate(ActorAct act);
	public event ActingContentCompleteDelegate ActingContentCompleteEvent;

	protected override void Start (){
		base.Start ();
		SetActingLength ();
		Diglbug.Log ("ADDING EVENT FOR ACTORACT");
		if (isTimeFromEnd) {
			lastTrackWithActingContent.AddEventAtTimeRemaining (ActingContentComplete, timeActingContentEnds);
		} else {
			lastTrackWithActingContent.AddEventAtTime (ActingContentComplete, timeActingContentEnds);
		}
	}

	private void SetActingLength(){
		float ret = 0f;
		for (int k = 0; k < trackEntries.Length; k++) {
			if (trackEntries [k] != lastTrackWithActingContent) {
				ret += trackEntries [k].GetTrackLength ();
			} else {
				if (isTimeFromEnd) {
					ret += trackEntries [k].GetTrackLength () - timeActingContentEnds;
				} else {
					ret += timeActingContentEnds;
				}
				break;
			}
		}
//		Diglbug.Log ("Set acting length: " + ret + " for " + name);
		actingLength = ret;
	}

	private void ActingContentComplete(){
		if (ActingContentCompleteEvent != null) {
			ActingContentCompleteEvent (this);
		}
	}

	public float GetActingLength(){
		return actingLength;
	}

	protected override string GetNameString(){
		return base.GetNameString() + " @ "+entryPayload;
	}

}