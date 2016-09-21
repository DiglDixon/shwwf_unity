using UnityEngine;

public class VibrateEvent : CustomTrackTimeEvent{

	public override void CustomEvent (){
		Handheld.Vibrate ();
	}

}