using UnityEngine;
using System.Collections.Generic;

public class LoopingTrack : AudioTrack{

	private bool shouldClone = true;
	private LoopingTrack clone;
	public float crossoverTime = 1f;

//	public override void AddEventAtTime (TrackEventDelegate newEvent, float eventTime)
//	{
//		Debug.Log ("Added Event to Looper "+name);
//		base.AddEventAtTime (newEvent, eventTime);
//		if (clone)
//			clone.AddEventAtTime (newEvent, eventTime);
//	}
//
//	public override void AddStateEventAtTime (TrackEventDelegate newEvent, float eventTime)
//	{
//		Debug.Log ("Added State Event to Looper "+name);
//		base.AddStateEventAtTime (newEvent, eventTime);
//		if (clone)
//			clone.AddStateEventAtTime (newEvent, eventTime);
//	}

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