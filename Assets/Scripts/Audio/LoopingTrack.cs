using UnityEngine;

public class LoopingTrack : AudioTrack{

	private bool shouldClone = true;
	private LoopingTrack clone;

	public override void AddEventAtTime(TrackEventDelegate newEvent, float occurOnTime){
		base.AddEventAtTime (newEvent, occurOnTime);
		if (clone)
			clone.AddEventAtTime (newEvent, occurOnTime);
	}

	public override void AddEventAtTimeRemaining(TrackEventDelegate newEvent, float occurOnTimeRemaining){
		base.AddEventAtTimeRemaining (newEvent, occurOnTimeRemaining);
		if (clone)
			clone.AddEventAtTimeRemaining (newEvent, occurOnTimeRemaining);
	}

	public void Clone(){
		if (shouldClone) {
			GameObject cloneObject = GameObject.Instantiate (gameObject) as GameObject;
			cloneObject.transform.SetParent (transform);
			clone = cloneObject.GetComponent<LoopingTrack> ();
			clone.DisableClone ();
			GetComponent<LoopingTracklistEntry> ().SetCloneTrack (clone);
		}
	}

	public void DisableClone(){
		shouldClone = false;
	}

}