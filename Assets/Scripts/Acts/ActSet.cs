using UnityEngine;

public class ActSet : EnsureDefinedActsInChildren<ShowAct>{

	private Act[] acts = new Act[0];

	public Act startingAct;
	public Act endingAct;

	private Act currentAct;

	public TracklistPlayer player;

	public delegate void ActChangedDelegate(Act newAct);
	public event ActChangedDelegate ActChangedEvent;

	public delegate void ActEndsDelegate(Act actEnded);
	public event ActEndsDelegate ActEndsEvent;

	private void Awake(){
		acts = GetComponentsInChildren<Act> ();
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
	}

}