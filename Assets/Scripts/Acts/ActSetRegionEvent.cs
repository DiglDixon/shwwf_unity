using UnityEngine;

public abstract class ActSetRegionEvent : MonoBehaviour{

	public TracklistEntry beginningEntry;
	public float beginAtTime;
	public bool beginAtTimeFromEnd;

	public TracklistEntry endingEntry;
	public float endAtTime;
	public bool endAtTimeFromEnd;

	private float actSetEntryTime;
	private float actSetExitTime;
	private bool hasEntered = false;

	public void SetActSetEntryTime(float t){
		actSetEntryTime = t;
	}

	public void SetActSetExitTime(float t){
		actSetExitTime = t;
	}

	public void HandleTimeUpdate(float newTime){
		if (ShoudExit (newTime)) {
			hasEntered = false;
			RegionExited ();
		} else if (ShouldEnter(newTime)){
			hasEntered = true;
			RegionEntered ();
		}
	}

	private bool ShouldEnter(float newTime){
		return ( (newTime > actSetEntryTime) && (newTime < actSetExitTime) && (!hasEntered) );
	}

	private bool ShoudExit(float newTime){
		return ( ( (newTime > actSetExitTime) || (newTime < actSetEntryTime) ) && (hasEntered) );
	}

	public abstract void RegionEntered();

	public abstract void RegionExited();

}
