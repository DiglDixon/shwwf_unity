using UnityEngine;
using System.Collections;

public class ModeSpecificObject : MonoBehaviour {

	public string modeName;
	public bool destroyIfIsMode = false;
	// Use this for initialization
	void Start () {


		if (destroyIfIsMode) {
			if (ShowMode.Instance.Mode.ModeName == modeName) {
				Destroy (gameObject);
			}
		} else {
			if (ShowMode.Instance.Mode.ModeName != modeName) {
				Destroy (gameObject);
			}
		}
	}
}
