using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMode : Mode{

	public GameObject signalDebuggerButton;

	public override ModeName ModeName {
		get {
			return ModeName.GUIDE;
		}
	}

	public override void BeginShow (){
		SceneManager.LoadScene (Scenes.MonoScene);
	}

	public override void ModeSelected (){
		BeginShow ();
		signalDebuggerButton.SetActive (true);
		Variables.Instance.debugBuild = true;
	}

	public override void ModeDeselected (){
		signalDebuggerButton.SetActive (false);
		Variables.Instance.debugBuild = false;
	}

	public override void NewSceneLoaded (Scene scene){
		EnableObjectsWithTag ("Audience");
		EnableObjectsWithTag ("Guide");
	}
}