using UnityEngine;
using System.Collections;

public class PhoneCallToMum :  AppControllerStopStartEvent {
	public override void FireBeginningEvent (){
		appController.PhoneToMumBegins ();
	}
	public override void FireEndEvent (){
		appController.PhoneToMumEnds ();
	}
}
