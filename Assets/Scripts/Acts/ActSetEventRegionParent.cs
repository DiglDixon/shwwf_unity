using UnityEngine;

public class ActSetEventRegionParent : MonoBehaviour{
	
	private ActSetRegionEvent[] regions;

	private void Awake(){
		regions = GetComponentsInChildren<ActSetRegionEvent> ();
	}

	public void UpdateTimeElapsed(float actTime){
		for (int k = 0; k < regions.Length; k++) {
			regions [k].HandleTimeUpdate (actTime);
		}
	}


}