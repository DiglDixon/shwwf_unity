using System;
using System.Collections;
using UnityEngine;


public class MobileBluetoothManager : BluetoothManager{

	private Signature signature;

	public PipeExample pipe;

	private bool validateReady = false;

	protected override void Start(){
		iBeaconReceiver.BeaconRangeChangedEvent += BeaconFoundEvent;
		BluetoothState.BluetoothStateChangedEvent += StateChangeEvent;
		#if !UNITY_EDITOR
		StartCoroutine(ValidateBluetooth());
		#endif
	}

	private void StateChangeEvent(BluetoothLowEnergyState state){
		Debug.Log ("State changed: " + state);
		validateReady = state == BluetoothLowEnergyState.POWERED_ON;
	}

	private IEnumerator ValidateBluetooth(){
		// this triggers the permission dialog
		float validateTimeoutMax = 10f;
		float time = 0f;
		while (time < validateTimeoutMax) {
			time += Time.deltaTime;
			if (validateReady) {
				Debug.Log ("Running validation...");
				StartReceiving();
//				StopReceiving (); // commenting this out for now, so we don't have to init for DM
				break;
			}
			yield return null;
		}
		if (time >= validateTimeoutMax) {
			Debug.Log ("Validation timed out.");
		}
	}

	public void EnableBluetooth(){
		BluetoothState.EnableBluetooth();
	}

	public override void StartReceiving(){
		Diglbug.LogMobile("Start rec", "PIPE");
		pipe.Digl_Stop();
		pipe.Digl_SetSwitch (BroadcastMode.receive);
		// The null beacon is set 
		pipe.Digl_Start ();
	}

	public override void StopSending(){
		Diglbug.LogMobile("Stop send", "PIPE");
		pipe.Digl_Stop();
	}

	public override void StopReceiving(){
		Diglbug.LogMobile("Stop rec", "PIPE");
		pipe.Digl_Stop();
	}

	public override void SendSignal(Signal s){
		Diglbug.LogMobile("Start send", "PIPE");
		pipe.Digl_Stop ();
		pipe.Digl_SetBeacon (s.ToBeacon ());
		pipe.Digl_SetSwitch (BroadcastMode.send);
		pipe.Digl_Start ();
		AutoAcceptOwnSignal (s);
	}

	private void AutoAcceptOwnSignal(Signal s){
		FireBeaconFoundEvent (s);
	}

	public void BeaconFoundEvent(Beacon[] beacons){
		Diglbug.LogMobile ("BeaconFoundEvent", "BFE");
		for (int k = 0; k < beacons.Length; k++) {
			int second = System.DateTime.Now.Second;
			Diglbug.LogMobile ((beacons [k].minor-1).ToString()+"@"+second, "REC:"+(beacons [k].major-1).ToString ());
		}

		Signal[] signals = Array.ConvertAll(beacons, item => new Signal(item));
		FireBeaconsFoundEvent (signals);
	}


}