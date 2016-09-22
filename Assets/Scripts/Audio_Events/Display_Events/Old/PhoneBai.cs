using UnityEngine;
using System.Collections;

public class PhoneBai :  AppControllerStopStartEvent {
	public override void FireBeginningEvent (){
		appController.PhoneBaiBegins ();
	}
	public override void FireEndEvent (){
		appController.PhoneBaiEnds ();
	}
}
