using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ModeSpecificObject : MonoBehaviour {

	public ModeName modeName;
	public bool destroyIfIsMode = false;

	public ObjectDisableType disableType = ObjectDisableType.DESTORY;

	// Use this for initialization
	void Start () {
		if (destroyIfIsMode) {
			if (ShowMode.Instance.Mode.ModeName == modeName) {
				Disable ();
			}
		} else {
			if (ShowMode.Instance.Mode.ModeName != modeName) {
				Disable();
			}
		}
	}
	private void Disable(){
		if (disableType == ObjectDisableType.DESTORY) {
			Destroy (gameObject);
		} else if (disableType == ObjectDisableType.DISABLE) {
			gameObject.SetActive (false);
		} else if (disableType == ObjectDisableType.INTERACTABLE_SLIDER) {
			gameObject.GetComponent<Slider> ().interactable = false;
		}
	}
}

public enum ObjectDisableType{
	DESTORY,
	DISABLE,
	INTERACTABLE_SLIDER
}