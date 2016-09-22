using UnityEngine;
using System.Collections;

public class TextLandlord :  AppControllerStopStartEvent {
	public override void FireBeginningEvent (){
		appController.LandlordTextBegins ();
	}
	public override void FireEndEvent (){
		appController.LandlordTextEnds ();
	}
}
