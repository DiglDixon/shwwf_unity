using System;
using UnityEngine;


public class MobileBluetoothManager : BluetoothManager{

	private Signature signature;

	public PipeExample pipe;

	protected override void Start(){
		iBeaconReceiver.BeaconRangeChangedEvent += BeaconFoundEvent;
//		BluetoothState.BluetoothStateChangedEvent += StateChangeEvent;
	}

	public void EnableBluetooth(){
		BluetoothState.EnableBluetooth();
	}

	public override void StartReceiving(){
		Diglbug.LogMobile("Start rec", "PIPE");
		pipe.Digl_Stop();
		pipe.Digl_SetSwitch (BroadcastMode.receive);
//		pipe.btn_changeUUID (
//			"com.storybox.twwf16",
//			SignalUtils.GetSignaureUUID (signature),
//			"0",
//			"0");
//		pipe.Digl_SetBeacon(
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
//		pipe.btn_changeUUID (
//			"com.storybox.twwf16",
//			SignalUtils.GetSignaureUUID (signature),
//			"13378",
//			"13378");

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