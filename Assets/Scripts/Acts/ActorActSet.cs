using UnityEngine;

public class ActorActSet : EnsureActSetChild{

	private ActorAct[] acts;

	public float actorContentEndTime = 1f;
	public bool isTimeRemaining = true;

	public delegate void SetCompleteDelegate(ActorActSet set);
	public event SetCompleteDelegate SetCompleteEvent;

	private EventTracklistEntry[] trackEntries = new EventTracklistEntry[0];

	private float totalActingLength;
	private float inverse_totalActingLength;

	public TracklistPlayer player;
	private ActorAct currentAct;
	public delegate void ActChangedDelegate(Act newAct);
	public event ActChangedDelegate ActChangedEvent;


	public delegate void ActContentCompleteDelegate(int index);
	public event ActContentCompleteDelegate ActContentCompleteEvent;

	public delegate void WaitingForNextActDelegate(ActorAct nextAct);
	public event WaitingForNextActDelegate WaitingForNextActEvent;

	private void Awake(){
		acts = GetComponentsInChildren<ActorAct> ();
		trackEntries = GetComponentsInChildren<EventTracklistEntry> ();

		for (int k = 0; k < acts.Length; k++) {
			acts [k].ActingContentCompleteEvent += ActingContentInActFinished;
		}
	}

	private void Start(){
//		if (trackEntries.Length > 0) {
//			EventTracklistEntry lastEntry = trackEntries [trackEntries.Length - 1];
//			Diglbug.Log ("Assigning lastEntry event "+name+": "+lastEntry.name, PrintStream.ACTORS);
//			if (isTimeRemaining) {
//				lastEntry.AddStateEventAtTimeRemaining (SetComplete, actorContentEndTime);
//			} else {
//				lastEntry.AddStateEventAtTime (SetComplete, actorContentEndTime);
//			}
//		} else {
//			Diglbug.Log ("Warning! ActorActSet " + name + " is empty - this should only be the case for testing", PrintStream.ACTORS);
//		}
	}

	public void InitialiseSet(){
		SetTotalActingLength ();
	}

	private void SetTotalActingLength(){
		totalActingLength = 0f;
		for (int k = 0; k < acts.Length; k++) {
			totalActingLength += acts [k].GetActingLength ();
		}
		inverse_totalActingLength = 1f / totalActingLength;
		Diglbug.Log ("Set acting length: " + totalActingLength + ", inverse: " + inverse_totalActingLength+" for "+name);
	}

	public TracklistEntry GetFirstTrackEntry(){
		if (trackEntries.Length == 0) {
			Diglbug.Log ("GetFirstTrackEntry requested but no trackEntires found for ActorActSet " + name, PrintStream.DEBUGGING);
			return null;
		} else {
			return trackEntries [0];
		}
	}

	public Act GetFirstAct(){
		return acts [0];
	}

	private void SetComplete(){
		Diglbug.Log ("ActorSet " + name + " complete!", PrintStream.ACTORS);
		if (SetCompleteEvent != null) {
			SetCompleteEvent (this);
		}
	}

	public ActorAct GetActPayloadStarts(Payload p){
		for(int k = 0; k<acts.Length; k++){
			if (acts [k].entryPayload == p) {
				return acts [k];
			}
		}
		return null;
	}

	protected override string GetNameString ()
	{
		return base.GetNameString ()+" ("+GetComponentsInChildren<ActorAct>().Length+")";
	}

	public float GetActingProgress(){
		if (currentAct != null) {
			float ret = 0f;
			for (int k = 0; k < acts.Length; k++) {
				if (acts [k] != currentAct) {
					ret += acts[k].GetActingLength ();
				} else {
					ret += Mathf.Min (acts[k].GetActTimeElapsed(), currentAct.GetActingLength ());
					break;
				}
			}
			return ret * inverse_totalActingLength;
		} else {
			return 0f;
		}
	}

	public float[] GetActMarkerPositions(){
		float[] ret = new float[acts.Length];

		for (int k = 0; k < acts.Length; k++) {
			ret [k] = acts [k].GetActingLength () / totalActingLength;
		}

		return ret;
	}

	/* Nasty C+P from ActSet - the Ensures are ruining inheritance */

	private void OnEnable(){
		player.NewTrackBeginsEvent += TrackBegins;
	}

	private void OnDisable(){
		player.NewTrackBeginsEvent -= TrackBegins;
	}

	public void TrackBegins(ITrack track){
		for (int k = 0; k < acts.Length; k++) {
			if (acts [k].ContainsTrack (track)) {
				TrackBeganInAct (track, acts [k]);
			}
		}
	}

	private void TrackBeganInAct(ITrack track, Act act){
		if (act != currentAct) {
			ChangeAct(act);
		}
	}

	private void ChangeAct(Act act){

		currentAct = (ActorAct)act;
		currentAct.ActChangedTo ();
		if (ActChangedEvent != null) {
			ActChangedEvent (currentAct);
		}
	}

	private int IndexOfAct(Act a){
		for (int k = 0; k < acts.Length; k++) {
			if (acts [k] == a) {
				return k;
			}
		}
		return -1;
	}

	private void ActingContentInActFinished(ActorAct act){
		int index = IndexOfAct (act);
		if (index != -1) {
			if (index == acts.Length - 1) {
				SetComplete ();
			} else if (index < acts.Length - 1) {
				if (WaitingForNextActEvent != null) {
					WaitingForNextActEvent (acts [index + 1]);
				}
			}
			if (ActContentCompleteEvent != null) {
				ActContentCompleteEvent (index);
			}
		} else {
			Diglbug.Log ("Unexpected act content finished message " + act.name + " in " + name);
		}
	}

	public ActorAct Rehearsal_GetNextAct(){
		int index = IndexOfAct (currentAct);
		if (index >= acts.Length - 1) {
			return null;
		} else {
			return acts [index + 1];
		}
	}

	public void Rehearse_SkipToProgress(float p){
		float destTime = totalActingLength * p;

		for (int k = 0; k < acts.Length; k++) {
			if (destTime - acts [k].GetActingLength () < 0f) {
				TracklistEntry entry = acts [k].GetEntryAtActTime (destTime);
				float delay = acts [k].GetSpecificEntryTimeAtActTime (entry, destTime);
				player.PlayTrackEntry(entry, delay);
				return;
			} else {
				destTime -= acts [k].GetActingLength ();
			}
		}
	}

}