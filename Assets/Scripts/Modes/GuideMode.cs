using UnityEngine;
using UnityEngine.SceneManagement;


public class GuideMode : Mode{

	private string modeName = "guide";

	public override string ModeName {
		get {
			return modeName;
		}
	}

	public override void BeginShow (){
		SceneManager.LoadScene (Scenes.Setup);
	}

	public override void ModeSelected (){
		BeginShow ();
	}

	public override void ModeDeselected (){

	}

	public override void NewSceneLoaded (Scene scene){
		EnableObjectsWithTag ("Audience");
		EnableObjectsWithTag ("Guide");
	}

}