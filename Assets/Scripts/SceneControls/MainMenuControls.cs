using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControls : MonoBehaviour{

	public void PasswordAttempted(string password){
		Diglbug.Log ("Password Attempted", PrintStream.SCENES);
		if(password.Equals(Passwords.ActorPassword)) {
			Diglbug.Log("Entering Actor Mode", PrintStream.SCENES);
			EnterActorMode ();
		}
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
