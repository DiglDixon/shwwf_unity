using UnityEngine;

public class BluetoothSetupStep : SetupStep{

	private bool bluetoothSignalFound = false;

	public GameObject[] enableWhilePlaying;
	public GameObject[] enableWhileStopped;

	public AudioSourceFadeControls audioControls;

	public override void Activate (ShowSetup callback){
		base.Activate (callback);
		BLE.Instance.DisableJockeyProtection ();
	}

	public void BluetoothFound(bool isStartSignal){
		if (isStartSignal) {
			audioControls.FadeTo (1f);

			SetArrayActive (enableWhilePlaying, true);
			SetArrayActive (enableWhileStopped, false);
		} else {
			audioControls.FadeTo (0f);
			SetArrayActive (enableWhilePlaying, false);
			SetArrayActive (enableWhileStopped, true);
		}
		if (isStartSignal) {
			bluetoothSignalFound = true;
		}
	}

	private void SetArrayActive(GameObject[] objects, bool value){
		for (int k = 0; k < objects.Length; k++) {
			if (objects [k] != null) {
				objects [k].SetActive (value);
			}
		}
	}

	protected override bool SetupCompleteCondition (){
		return bluetoothSignalFound;
	}

	protected override void ResetConditions (){
		bluetoothSignalFound = false;
		//		audioControls.FadeTo (0f);
		SetArrayActive (enableWhilePlaying, true);
		SetArrayActive (enableWhileStopped, false);
	}

}
