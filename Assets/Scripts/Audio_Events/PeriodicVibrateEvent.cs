using System.Collections;
using UnityEngine;

public class PeriodicVibrateEvent : CustomTrackTimeEvent{

	public float delay;

	public override void CustomEvent (){
		StartCoroutine ("RunVibrate");
	}

	public void StopVibration(){
		StopCoroutine ("RunVibrate");
	}

	private IEnumerator RunVibrate(){
		while (true) {
			Handheld.Vibrate ();
			yield return new WaitForSeconds (delay);
		}
	}


}