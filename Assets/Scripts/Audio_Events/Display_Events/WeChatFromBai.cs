using UnityEngine;
using System.Collections;

public class WeChatFromBai :  AppControllerStopStartEvent {
	public override void FireBeginningEvent (){
		appController.WeChatFromBaiBegins ();
	}
	public override void FireEndEvent (){
		appController.WeChatFromBaiEnds ();
	}
}
