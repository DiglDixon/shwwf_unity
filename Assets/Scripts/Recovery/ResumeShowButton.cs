using UnityEngine;
using System.Collections;

public class ResumeShowButton : MonoBehaviour {

	void Start () {
		gameObject.SetActive (RecoveryManager.Instance.ResumeAvailable ());
		if (gameObject.activeSelf) {
			GetComponent<CanvasGroup> ().alpha = 1f;
		}
	}

	public void ButtonPressed(){
		RecoveryManager.Instance.ResumeRequested ();
	}
}
