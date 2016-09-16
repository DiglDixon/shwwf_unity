
using UnityEngine;
using System.Collections;

public class WeChatVoiceFromBai :  AppControllerStopStartEvent {
	public override void FireBeginningEvent (){
		appController.WeChatVoiceFromBaiBegins ();
	}
	public override void FireEndEvent (){
		appController.WeChatVoiceFromBaiBegins ();
	}
}
