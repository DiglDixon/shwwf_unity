using System;
using System.Collections;

public static class Utils{
	
	public static bool[] booleanArray(int length, bool startValue){
		bool[] ret = new bool[length];
		for (int k = 0; k < ret.Length; k++) {
			ret [k] = startValue;
		}
		return ret;
	}

	public static string AudioTimeFormat(float v){
		TimeSpan t = TimeSpan.FromSeconds(v);
		return string.Format("{0:D2}:{1:D2}:{2:D2}",
			t.Minutes, 
			t.Seconds, 
			t.Milliseconds);
	}

}