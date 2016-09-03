using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectorMode : Mode{


	public override string ModeName {
		get{
			return "director";
		}
	}

	public override void ModeSelected (){
		SceneManager.LoadScene (Scenes.DirectorMode);
	}

	public override void ModeDeselected (){

	}

	public override void BeginShow (){

	}

	public override void NewSceneLoaded (Scene scene){

	}
}