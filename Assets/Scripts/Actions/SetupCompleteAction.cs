

public class SetupCompleteAction : PayloadEventAction{

	public WaitForStartSetupStep waitStep;

	public override void FireEvent (Signal s){
		waitStep.FinishSetupReceived ();
	}

}
