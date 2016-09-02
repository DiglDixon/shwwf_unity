using UnityEngine;
using UnityEngine.SceneManagement;

public class PasswordControls : MonoBehaviour{

	public void PasswordAttempted(string password){
		if (ShowMode.Instance.PasswordIsValid (password)) {
			ShowMode.Instance.SetModeByPassword (password);
			PasswordAttemptSuccessful ();
		} else {
			PasswordAttemptFailed (password);
		}
	}

	private void PasswordAttemptFailed(string password){
		Diglbug.Log ("Password attempt failed: "+password);
	}

	private void PasswordAttemptSuccessful(){

	}

	public void EnterDirectorMode(){
		SceneManager.LoadScene ("director");
	}

	public void EnterActorMode(){
		SceneManager.LoadScene ("actor");
	}

	public void EnterAudienceMode(){
		SceneManager.LoadScene ("setup");
	}

	public void EnterGuideMode(){
		SceneManager.LoadScene ("setup");
	}

}
