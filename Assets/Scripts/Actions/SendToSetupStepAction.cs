
public class SendToSetupStepAction : PayloadEventAction{

	public ReceiveSignalSetupStep setupStep;

	public override void FireEvent (Signal s){
		setupStep.ReceiveSignal (s);
	}

	protected override string GetGameObjectName (){
		return ("ForwardTo: " + setupStep.name);
	}


}