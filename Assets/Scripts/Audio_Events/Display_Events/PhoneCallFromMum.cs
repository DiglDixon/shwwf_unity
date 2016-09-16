using UnityEngine;
using System.Collections;

public class PhoneCallFromMum :  AppControllerStopStartEvent {
	public override void FireBeginningEvent (){
		appController.PhoneFromMumBegins ();
	}
	public override void FireEndEvent (){
		appController.PhoneFromMumEnds ();
	}
}
