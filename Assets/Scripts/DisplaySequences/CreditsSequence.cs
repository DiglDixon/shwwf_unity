using UnityEngine;
using System.Collections;


public class CreditsSequence : MonoBehaviour{

	public GameObject showWhenStarted;
	public SequenceObject background;

	public void Update(){
		if (Input.GetKeyDown (KeyCode.C)) {
			Begin ();
		}
		if (Input.GetKeyDown (KeyCode.V)) {
			Cancel ();
		}
	}

	public void Begin(){
		Diglbug.Log ("Beginning Credits");
		showWhenStarted.SetActive (true);
		background.BeginSequence ();
	}

	public void Cancel(){
		Diglbug.Log ("Cancelling Credits");
		showWhenStarted.SetActive (false);
		background.CancelSequence ();
	}


}
