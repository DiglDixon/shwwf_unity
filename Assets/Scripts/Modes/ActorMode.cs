using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ActorMode : Mode{

	private string modeName = "actor";
	public override string ModeName {
		get {
			return modeName;
		}
	}

	private void ActBegins(Act a){

	}

	public void ActEnds(Act a){

	}

	public override void BeginShow (){
		SceneManager.LoadScene (Scenes.Actor);
	}

	public override void ModeDeselected (){
	}

	public override void ModeSelected (){
		BeginShow ();
	}

	public override void NewSceneLoaded (UnityEngine.SceneManagement.Scene scene){
		Diglbug.Log ("Scene "+scene.name+" begins for " + ModeName, PrintStream.SCENES);
	}



}