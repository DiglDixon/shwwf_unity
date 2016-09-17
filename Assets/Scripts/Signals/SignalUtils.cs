using System;
using UnityEngine;

public static class SignalUtils{
	
	public static Signal NullSignal{
		get{
			return new Signal (Signature.NONE, Payload.NONE);
		}
	}

	public static Signature[] GetAllSignatures (){
		Signature[] ret = new Signature[Enum.GetValues(typeof(Signature)).Length];
		for(int k = 0; k<ret.Length; k++){
			ret[k] = (Signature)k;
		}
		return ret;
	}

	private static readonly string[] uuids = {
		"5ff99f07-d609-4553-9dfb-37028ebd49bc",
		/*"ea742c3c-3497-4cd6-a6c6-a276408cb4cf",
		"aec3df34-0445-412a-9fc6-1eb3c07748d6",
		"cca5270c-a586-4b24-858e-fd08c6038f8e",
		"9f5028ff-0cf7-40a0-842c-9f9aa48e8dc6",
		"aeb88bf2-ad75-4d95-83f4-19d523a437f3",
		"75700bbe-fc8f-41e5-b603-a5e7386b5a48",
		"e29e4015-2700-44ed-ae4c-92340b27dbe3",
		"291aa227-25c7-4e13-b956-3ce118140a22",
		"c9ab85d8-f017-42f7-9c49-7e563e3a165c"*/ // REMOVING these, going back to single UUID.
	};

	public static string GetSignaureUUID(Signature signature){
		return uuids[ ( (int)signature % uuids.Length) ];
	}

	public static int GetSignalTimeOffset(SignalTime s){

		DateTime now = System.DateTime.Now;


		int nowMinute = now.Minute;
		int prevMinute = s.minute;

		// If the current minute is smaller than the previous one, we must have ticked an hour
		if (nowMinute < prevMinute) {
			nowMinute += 60;
		} 

		int minuteDifference = nowMinute - prevMinute;
		if (minuteDifference == 0) {
			return now.Second - s.second;
		} else {
			int ret = (60 - s.second) + now.Second + 60 * (Mathf.Max (minuteDifference - 1, 0));
			return ret;
		}
	}

	public static SignalTime GetSignalTime(){
		return new SignalTime (
			System.DateTime.Now.Minute,
			System.DateTime.Now.Second
		);
	}

}