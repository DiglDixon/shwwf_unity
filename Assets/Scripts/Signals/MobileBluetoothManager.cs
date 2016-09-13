using System;
using System.Collections;
using UnityEngine;


public class MobileBluetoothManager : BluetoothManager{

	public PipeExample pipe;

	private bool validating = false;
	private bool validateReady = false;

	private float timePassedSinceLastReception = 0f;
	private float pluginReinitialiseTimeout = 4f;
	private bool isReceiving = false;

	private bool canSend = false;

	private void Awake(){
	}

	protected override void Start(){
		base.Start ();
		iBeaconReceiver.BeaconRangeChangedEvent += BeaconFoundEvent;
		BluetoothState.BluetoothStateChangedEvent += StateChangeEvent;
		#if !UNITY_EDITOR
		StartCoroutine(ValidateBluetooth());
		#endif
	}

	private void StateChangeEvent(BluetoothLowEnergyState state){
		Debug.Log ("State changed: " + state);
		canSend = iBeaconServer.checkTransmissionSupported();
		validateReady = state == BluetoothLowEnergyState.POWERED_ON;
		if (state == BluetoothLowEnergyState.POWERED_OFF) {
			Diglbug.Log ("BLE OFF. Asking the user to switch it back on.", PrintStream.SIGNALS);
			BluetoothState.EnableBluetooth ();
		}
		if (state != BluetoothLowEnergyState.POWERED_ON && state!= BluetoothLowEnergyState.TURNING_ON) {
			Diglbug.Log ("BLE OFF. Asking the user to switch it back on.", PrintStream.SIGNALS);
			if (validating == false) {
				StartCoroutine (ValidateBluetooth ());
			}
		}
	}

	private IEnumerator ValidateBluetooth(){
		// this triggers the permission dialog
		validating = true;
//		float validateTimeoutMax = 10f;
		float time = 0f;
		yield return new WaitForSeconds (1f);
		while (true/*time < validateTimeoutMax*/) {
			
			time += Time.deltaTime;

			if (validateReady) {
				Debug.Log ("Running validation...");
				bool shouldStop = (isReceiving == false);
				StartReceiving();
				if (shouldStop) {
					StopReceiving ();
				}
				break;
			}

			yield return null;
		}

//		if (time >= validateTimeoutMax) {
//			Debug.Log ("Validation timed out.");
//			break;
//		}

		validating = false;
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
		ActivateReinitialiseCountdown ();
		ResetReinitialiseCountdown ();
	}

	#if UNITY_IOS
	private void Update(){
		if (isReceiving) {
			timePassedSinceLastReception += Time.deltaTime;
			if (timePassedSinceLastReception > pluginReinitialiseTimeout) {
				Diglbug.Log ("Reinitialising iOS BLE Rec", PrintStream.SIGNALS);
				ResetReinitialiseCountdown ();
				StartReceiving (); // re-trigger the StartReceiving, which shoudl reinitialise the plugin.
			}
		}
	}
	#endif

	private void ResetReinitialiseCountdown(){
		Diglbug.Log ("Reinitialise Countdown Reset", PrintStream.SIGNALS);
		timePassedSinceLastReception = 0f;
	}

	private void ActivateReinitialiseCountdown(){
		isReceiving = true;
		ResetReinitialiseCountdown ();
	}

	private void DisableReinitialiseCountdown(){
		ResetReinitialiseCountdown ();
		isReceiving = false;
	}

	public override void StopSending(){
		Diglbug.LogMobile("Stop send", "PIPE");
		pipe.Digl_Stop();
		DisableReinitialiseCountdown ();
	}

	public override void StopReceiving(){
		Diglbug.LogMobile("Stop rec", "PIPE");
		pipe.Digl_Stop();
		DisableReinitialiseCountdown ();
	}

	public override void SendSignal(Signal s){
		Diglbug.LogMobile ("Start send", "PIPE");
		if (canSend) {
			pipe.Digl_Stop ();
			pipe.Digl_SetSendBeacon (s.ToBeacon ());
			pipe.Digl_SetSwitch (BroadcastMode.send);
			pipe.Digl_Start ();
		}
		AutoAcceptOwnSignal (s);
		DisableReinitialiseCountdown ();
	}

	private void AutoAcceptOwnSignal(Signal s){
		FireBeaconFoundEvent (s);
	}

	public void BeaconFoundEvent(Beacon[] beacons){
		Diglbug.LogMobile ("BeaconFoundEvent "+beacons.Length, "BFE");
		for (int k = 0; k < beacons.Length; k++) {
			int second = System.DateTime.Now.Second;
			Diglbug.LogMobile ((beacons [k].minor-1).ToString()+"@"+second, "REC:"+(beacons [k].major-1).ToString ());
		}

		Signal[] signals = Array.ConvertAll(beacons, item => new Signal(item));
		FireBeaconsFoundEvent (signals);

		ResetReinitialiseCountdown ();
	}

//	public override void SetReceivedSignatures (Signature[] ss){
//		base.SetReceivedSignatures (ss);
//
////		bool resumeRec = isReceiving;
////		if (resumeRec) {
////			StopReceiving ();
////		}
////
////		pipe.Digl_SetReceiveSignatures (ss);
////
////		if (resumeRec) {
////			StartReceiving ();
////		}
//	}


}