using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectorMode : Mode{

	private string modeName = "director";

	public override string ModeName {
		get{
			return modeName;
		}
	}

	public override void ModeSelected (){
		BeginShow ();
	}

	public override void ModeDeselected (){

	}

	public override void BeginShow (){
		SceneManager.LoadScene (Scenes.MonoScene);
	}

	public override void NewSceneLoaded (Scene scene){
		EnableObjectsWithTag ("Director");
	}
}