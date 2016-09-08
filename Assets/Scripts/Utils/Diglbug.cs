using System;
using UnityEngine;

public static class Diglbug{

	private static bool[] streamPrintEnabled = Utils.booleanArray(Enum.GetValues(typeof(PrintStream)).Length, true);

	public static void SetAllPrintStream(bool active){
		if (active) {
			Diglbug.Log ("Enabled all PrintStreams", PrintStream.PRINT_STREAMS);
		} else {
			Diglbug.Log ("Disabled all PrintStreams", PrintStream.PRINT_STREAMS);
		}
		streamPrintEnabled = Utils.booleanArray(Enum.GetValues(typeof(PrintStream)).Length, active);
	}

	public static void EnablePrintStream(PrintStream stream){
		streamPrintEnabled [(int)stream] = true;
		Diglbug.Log ("Enabled print stream " + stream, stream);
	}

	public static void DisablePrintStream(PrintStream stream){
//		Diglbug.Log ("Disabled print stream " + stream, PrintStream.PRINT_STREAMS);
		streamPrintEnabled [(int)stream] = false;
	}

	public static void Log(string message){
		Log(message, PrintStream.UNDEFINED);
	}

	public static void Log(string message, PrintStream stream){
		if (streamPrintEnabled [(int)stream])
			PrintToUnityLog ("[" + stream + "] " + message);
	}

	public static void LogWarning(string message){
		Debug.LogWarning (message);
	}

	public static void LogError(string message){
		Debug.LogError (message);
	}

	private static void PrintToUnityLog(string s){
		Debug.Log (s);
	}

	private static MobileDebugger mobileDebugger;

	public static void LogMobile(string message, string channel){
		if (!Variables.debugBuild) {
			return;
		}
		if (mobileDebugger == null) {
			GameObject obj = GameObject.Instantiate (Resources.Load ("Mobile_Debugger")) as GameObject;
			obj.transform.SetAsLastSibling ();
			mobileDebugger = obj.GetComponentInChildren<MobileDebugger> ();
		}
		mobileDebugger.LogMessage (message, channel);
	}
		
	// A polling-reminder would be cool. For things like temp keydowns. Pings every interval if available.

}
