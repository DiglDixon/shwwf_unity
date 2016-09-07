

public class BluetoothTestAction : PayloadEventAction {

	public BluetoothSetupStep bleSetupStep;
	public bool isStartSignal;

	public override void FireEvent (Signal s){
		bleSetupStep.BluetoothFound (isStartSignal);
	}

}
