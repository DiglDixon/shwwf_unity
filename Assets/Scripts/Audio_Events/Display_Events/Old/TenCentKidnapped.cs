using UnityEngine;
using System.Collections;

public class TenCentKidnapped :  AppControllerStopStartEvent {
	public override void FireBeginningEvent (){
		appController.TenCentKidnapBegins();
	}
	public override void FireEndEvent (){
		appController.TenCentKidnapEnds ();
	}
}
