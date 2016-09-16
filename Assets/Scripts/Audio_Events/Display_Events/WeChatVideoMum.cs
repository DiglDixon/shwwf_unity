using UnityEngine;
using System.Collections;

public class WeChatVideoMum :  AppControllerStopStartEvent {
	public override void FireBeginningEvent (){
		appController.WeChatVideoMumBegins ();
	}
	public override void FireEndEvent (){
		appController.WeChatVideoMumEnds ();
	}
}
