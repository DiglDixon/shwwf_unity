using UnityEngine;
using UnityEngine.SceneManagement;


public class GuideMode : Mode{

	public override ModeName ModeName {
		get {
			return ModeName.GUIDE;
		}
	}

	public override void BeginShow (){
			SceneManager.LoadScene (Scenes.MonoScene);
	}

public override void ModeSelected (){
		if (!RecoveryManager.Instance.RunningRecovery ()) {
			BeginShow ();
		}
	}

	public override void ModeDeselected (){

	}

	public override void NewSceneLoaded (Scene scene){
		EnableObjectsWithTag ("Audience");
		EnableObjectsWithTag ("Guide");
	}

}