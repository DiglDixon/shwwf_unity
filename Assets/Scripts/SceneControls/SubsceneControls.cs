using UnityEngine;
using UnityEngine.SceneManagement;

public class SubsceneControls : MonoBehaviour{

	public void ReturnToMainScene(){
		Diglbug.Log ("Returning to main_menu", PrintStream.SCENES);
		SceneManager.LoadScene ("main_menu");
	}

}
