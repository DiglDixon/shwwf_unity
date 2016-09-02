using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenuButton : MonoBehaviour {

	public void ReturnToMainMenu(){
		SceneManager.LoadScene ("main_menu");
	}

}
