using UnityEngine;
using System.Collections;

public class ResumeShowButton : MonoBehaviour {

	void Start () {
		gameObject.SetActive (RecoveryManager.Instance.ResumeAvailable ());
	}

	public void ButtonPressed(){
		RecoveryManager.Instance.ResumeRequested ();
	}
}
