using UnityEngine;

public class WaitForStartSetupStep : SetupStep{

	private bool startSignalFound = false;

	protected override void Update (){
		base.Update ();
		if (Input.GetKeyDown (KeyCode.G)) {
			startSignalFound = true;
		}
	}

	protected override bool SetupCompleteCondition (){
		return startSignalFound;
	}

	protected override void ResetConditions (){
		startSignalFound = false;
	}

}