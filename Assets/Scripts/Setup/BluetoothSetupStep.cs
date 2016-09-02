using UnityEngine;

public class BluetoothSetupStep : SetupStep{

	private bool bluetoothSignalFound = false;

	protected override bool SetupCompleteCondition (){
		return bluetoothSignalFound;
	}

	protected override void Update (){
		base.Update ();

		if (Input.GetKeyDown (KeyCode.B)) {
			Diglbug.Log ("Faked bluetooth setup signal received", PrintStream.DEBUGGING);
			bluetoothSignalFound = true;
		}
	}

	protected override void ResetConditions (){
		bluetoothSignalFound = false;
	}

}
