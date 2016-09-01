using System;
using UnityEngine;

public static class Diglbug{

	private static bool[] streamPrintEnabled = Utils.booleanArray(Enum.GetValues(typeof(PrintStream)).Length, true);

	public static void EnablePrintStream(PrintStream stream){
		streamPrintEnabled [(int)stream] = true;
		Diglbug.Log ("Enabled print stream " + stream, PrintStream.PRINT_STREAMS);
	}

	public static void DisablePrintStream(PrintStream stream){
		Diglbug.Log ("Disabled print stream " + stream, PrintStream.PRINT_STREAMS);
		streamPrintEnabled [(int)stream] = false;
	}

	public static void Log(string message){
		Log(message, PrintStream.UNDEFINED);
	}

	public static void Log(string message, PrintStream stream){
		if (streamPrintEnabled [(int)stream])
			PrintToUnityLog ("[" + stream + "] " + message);
	}

	public static void LogError(string message){
		Debug.LogError (message);
	}

	private static void PrintToUnityLog(string s){
		Debug.Log (s);
	}


}
