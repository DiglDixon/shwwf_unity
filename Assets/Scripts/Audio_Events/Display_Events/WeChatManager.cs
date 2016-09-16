using UnityEngine;
using System.Collections;

public class WeChatManager :  AppControllerStopStartEvent {
	public override void FireBeginningEvent (){
		appController.WeChatManagerBegins ();
	}
	public override void FireEndEvent (){
		appController.WeChatManagerEnds ();
	}
}
