using UnityEngine;

public class ApplicationInits : MonoBehaviour{
	
	private void Start(){
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Application.runInBackground = true;// This was in TWWF. Not sure if it works.
	}
}