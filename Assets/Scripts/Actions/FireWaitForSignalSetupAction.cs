
public class FireWaitForSignalSetupAction : PayloadEventAction{

	public WaitForPayloadSetupStep waitForPayloadStep;

	public override void FireEvent (Signal s){
		waitForPayloadStep.SignalReceived ();
	}

}