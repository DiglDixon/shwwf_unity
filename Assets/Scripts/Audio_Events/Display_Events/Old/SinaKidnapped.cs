using UnityEngine;
using System.Collections;

public class SinaKidnapped :  AppControllerStopStartEvent {
	public override void FireBeginningEvent (){
		appController.SinaKidnappedBegins ();
	}
	public override void FireEndEvent (){
		appController.SinaKidnappedEnds ();
	}
}
