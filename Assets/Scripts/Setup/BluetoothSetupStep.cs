using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class BluetoothSetupStep : SetupStep{

	private bool bluetoothSignalFound = false;
	private AudioSource source;

	private void Start(){
		source = GetComponent<AudioSource> ();
		source.clip.LoadAudioData ();
	}

	public void BluetoothFound(bool isStartSignal){
		if (isStartSignal) {
			source.Play ();
		} else {
			source.Stop ();
		}
		if (isStartSignal) {
			bluetoothSignalFound = true;
		}
	}

	protected override bool SetupCompleteCondition (){
		return bluetoothSignalFound;
	}

	protected override void ResetConditions (){
		bluetoothSignalFound = false;
		source.Stop ();
	}

}
