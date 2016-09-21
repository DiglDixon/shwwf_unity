using UnityEngine;

public class ActSet : EnsureDefinedActsInChildren<ShowAct>{

	private Act[] acts = new Act[0];

	public Act startingAct;
	public Act endingAct;

	private Act currentAct;

	public TracklistPlayer player;

	public delegate void ActChangedDelegate(Act newAct);
	public event ActChangedDelegate ActChangedEvent;

	public delegate void FinalActBeginsDelegate(Act finalAct);
	public event FinalActBeginsDelegate FinalActBeginsEvent;

//	public ActSetEventRegionParent eventsParent;
//	public bool eventsEnabled = false;

	private void Awake(){
		acts = GetComponentsInChildren<Act> ();
	}

	public void InitialiseActs(){
		for (int k = 0; k < acts.Length; k++) {
			acts [k].Initialise ();
		}
	}

//	public void DisableEvents(){
//		eventsEnabled = false;
//	}
//
//	public void EnableEvents(){
//		eventsEnabled = true;
//	}
//
//	private void Update(){
//		if (eventsEnabled) {
//			eventsParent.UpdateTimeElapsed (GetActSetTimeElapsed ());
//		}
//	}

	public float GetActSetTimeElapsed(){
		if (currentAct != null) {
			float total = 0f;
			for (int k = 0; k < acts.Length; k++) {
				if (currentAct == acts [k]) {
					total += acts [k].GetActTimeElapsed ();
					break;
				} else {
					total += acts [k].GetTotalActTime ();
				}
			}
			return total;
		}
		return 0f;
	}

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
		currentAct = act;
		currentAct.ActChangedTo ();
		if (ActChangedEvent != null) {
			ActChangedEvent (currentAct);
		}
		if(currentAct == acts[acts.Length-1]){
			if(FinalActBeginsEvent != null){
				FinalActBeginsEvent(currentAct);
			}
		}
	}

	public Payload GetPayloadForDefinedAct(DefinedAct a){
		for(int k = 0; k<acts.Length; k++){
			if (acts [k].definedAct == a) {
				return acts [k].entryPayload;
			}
		}
		Diglbug.LogWarning ("GetPayloadForDefinedAct request returned empty! " + a);
		return Payload.NONE;
	}

	public DefinedAct GetDefinedActForPayload(Payload p){
		Act ret = GetActForPayload (p);
		if (ret == null) {
			Diglbug.LogWarning ("GetDefinedActForPayload request returned empty! " + p);
			return DefinedAct.ACT_SHOW_START;
		} else {
			return ret.definedAct;
		}
	}

	public Act GetActForPayload(Payload p){
		for(int k = 0; k<acts.Length; k++){
			if (acts [k].entryPayload == p) {
				return acts [k];
			}
		}
		Diglbug.LogWarning ("GetActForPayload request returned empty! " + p);
		return null;
	}

	public Act GetActForDefinedAct(DefinedAct a){
		for(int k = 0; k<acts.Length; k++){
			if (acts [k].definedAct == a) {
				return acts [k];
			}
		}
		Diglbug.LogWarning ("GetActForDefinedAct request returned empty! " + a);
		return null;
	}

}