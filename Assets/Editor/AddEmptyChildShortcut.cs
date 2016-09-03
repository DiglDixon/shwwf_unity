using UnityEngine;
using UnityEditor;

/// <summary>
/// From http://forum.unity3d.com/threads/how-to-create-new-gameobject-as-child-in-hierarchy.77444/
/// </summary>
public class AddEmptyChildShortcut : MonoBehaviour {
	[MenuItem("GameObject/Create Empty Child #&n")]
	static void BringMeBackFromTheEdgeOfMadness() {
		GameObject go = new GameObject("GameObject");
		if(Selection.activeTransform != null)
			go.transform.parent = Selection.activeTransform;
	}
}