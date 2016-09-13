//#if !UNITY_EDITOR
#if UNITY_IOS
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public static class PostProcessBuildTrigger {
	[PostProcessBuild]
	private static void OnPostProcessBuild(BuildTarget target, string path) {
		// Get plist
		string plistPath = path + "/Info.plist";
		PlistDocument plist = new PlistDocument();
		plist.ReadFromFile(plistPath);

		// Get root
		PlistElementDict rootDict = plist.root;

		// Set location usage description
		var usageDescription = PlayerPrefs.GetString("NSLocationUsageDescription", "DUMMY, bitte in Scene ändern");
		rootDict.SetString("NSLocationUsageDescription", usageDescription);
		rootDict.SetString("NSLocationAlwaysUsageDescription", usageDescription);
		rootDict.values.Remove("NSLocationWhenInUseUsageDescription");

		// Write to file
		plist.WriteToFile(plistPath);
	}
}
#endif
//#endif
