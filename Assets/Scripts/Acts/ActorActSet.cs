using UnityEngine;

public class ActorActSet : EnsureActorSetChild{

	private ActorAct[] acts;

	public float actorContentEndTime = 1f;
	public bool isTimeRemaining = true;

	public delegate void SetCompleteDelegate(ActorActSet set);
	public event SetCompleteDelegate SetCompleteEvent;

	private EventTracklistEntry[] trackEntries = new EventTracklistEntry[0];

	private void Awake(){
		acts = GetComponentsInChildren<ActorAct> ();
		trackEntries = GetComponentsInChildren<EventTracklistEntry> ();
	}

	private void Start(){
		if (trackEntries.Length > 0) {
			EventTracklistEntry lastEntry = trackEntries [trackEntries.Length - 1];
			Diglbug.Log ("Assigning lastEntry event "+name+": "+lastEntry.name, PrintStream.ACTORS);
			if (isTimeRemaining) {
				lastEntry.AddStateEventAtTimeRemaining (SetComplete, actorContentEndTime);
			} else {
				lastEntry.AddStateEventAtTime (SetComplete, actorContentEndTime);
			}
		} else {
			Diglbug.Log ("Warning! ActorActSet " + name + " is empty - this should only be the case for testing", PrintStream.ACTORS);
		}
	}

	public void BeginSet(){
		acts [0].Begin ();
	}

	public TracklistEntry GetFirstTrackEntry(){
		if (trackEntries.Length == 0) {
			Diglbug.Log ("GetFirstTrackEntry requested but no trackEntires found for ActorActSet " + name, PrintStream.DEBUGGING);
			return null;
		} else {
			return trackEntries [0];
		}
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

}