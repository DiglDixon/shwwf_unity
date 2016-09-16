using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PasswordControls : MonoBehaviour{

	public void PasswordAttempted(string password){
		ShowMode.Instance.PasswordEntered (password);
		if (ShowMode.Instance.PasswordIsValid (password)) {
			//
		} else {
			PasswordAttemptFailed (password);
		}
	}

	private void PasswordAttemptFailed(string password){
		// do things
		Diglbug.Log ("Password attempt failed: "+password);
	}

}
