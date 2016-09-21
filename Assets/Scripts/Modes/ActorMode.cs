using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ActorMode : Mode{
	
	public override ModeName ModeName {
		get {
			return ModeName.ACTOR;
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
		if (!RecoveryManager.Instance.RunningRecovery ()) {
			Signature[] sigs = SignalUtils.GetAllSignatures ();
			BLE.Instance.Manager.SetReceivedSignatures (sigs);
			BLE.Instance.Manager.StartReceiving ();
			BeginShow ();
		}
	}

	public override void NewSceneLoaded (UnityEngine.SceneManagement.Scene scene){
	}



}