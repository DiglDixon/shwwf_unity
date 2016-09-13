
public class WaitForPayloadSetupStep : SetupStep{

	private bool signalFound = false;

	public virtual void SignalReceived(){
		signalFound = true;
	}

	protected override bool SetupCompleteCondition (){
		return signalFound;
	}

	protected override void ResetConditions(){
		signalFound = false;
	}
}