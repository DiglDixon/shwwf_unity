using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectorMode : Mode{

	public override ModeName ModeName {
		get{
			return ModeName.DIRECTOR;
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