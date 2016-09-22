using UnityEngine;
using System.Collections;

public class Yaozhibo :  AppControllerStopStartEvent {
	public override void FireBeginningEvent (){
		appController.YaozhiboBegins ();
	}
	public override void FireEndEvent (){
		appController.YaozhiboEnds ();
	}
}
