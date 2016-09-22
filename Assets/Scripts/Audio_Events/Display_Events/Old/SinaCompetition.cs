using UnityEngine;
using System.Collections;

public class SinaCompetition :  AppControllerStopStartEvent {
	public override void FireBeginningEvent (){
		appController.SinaCompetitionBegins ();
	}
	public override void FireEndEvent (){
		appController.SinaCompetitionEnds ();
	}
}
