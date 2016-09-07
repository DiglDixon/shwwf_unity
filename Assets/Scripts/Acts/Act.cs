using UnityEngine;

public class Act : EnsurePayloadChild{

	public string ActName;
	private EventTracklistEntry[] actEntries;

	public TracklistPlayer player;

	public Payload entryPayload;

	private void Start(){
		actEntries = GetComponentsInChildren<EventTracklistEntry> ();
	}

	public void BeginAct(){
		player.PlayTrackEntry (actEntries [0]);
	}

	public override void SetPayload(Payload p){
		entryPayload = p;
	}

	public override Payload GetPayload(){
		return entryPayload;
	}

	protected override string GetNameString (){
		return "Act_" + entryPayload.ToString ();
	}


}