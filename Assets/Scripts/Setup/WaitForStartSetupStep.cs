using UnityEngine;

public class WaitForStartSetupStep : WaitForPayloadSetupStep{

	public GameObject beginButton;

	public override void Activate (ShowSetup callback){
		base.Activate (callback);
		if (ShowMode.Instance.Mode.ModeName != ModeName.AUDIENCE) {
			beginButton.SetActive (true);
		}
	}

	public override void Deactivate (){
		base.Deactivate ();
		beginButton.SetActive (false);
	}

}