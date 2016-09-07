using UnityEngine;

public class WaitForStartSetupStep : SetupStep{

	private bool startSignalFound = false;

	public void FinishSetupReceived(){
		startSignalFound = true;
	}

	protected override bool SetupCompleteCondition (){
		return startSignalFound;
	}

	protected override void ResetConditions (){
		startSignalFound = false;
	}

}