using UnityEngine;
using System;
using System.Reflection;
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

	public static T GetCopyOf<T>(this Component comp, T other) where T : Component
	{
		Type type = comp.GetType();
		if (type != other.GetType()) return null; // type mis-match
		BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
		PropertyInfo[] pinfos = type.GetProperties(flags);
		foreach (var pinfo in pinfos) {
			if (pinfo.CanWrite) {
				try {
					pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
				}
				catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
			}
		}
		FieldInfo[] finfos = type.GetFields(flags);
		foreach (var finfo in finfos) {
			finfo.SetValue(comp, finfo.GetValue(other));
		}
		return comp as T;
	}

}