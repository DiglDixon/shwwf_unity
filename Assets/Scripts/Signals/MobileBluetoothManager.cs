using System;
using UnityEngine;


public class MobileBluetoothManager : BluetoothManager{

	protected override void Start(){
		base.Start ();
		SetServerRegionData (SignalUtils.NullSignal);
		SetReceiverSignature (Signature.NONE);

		iBeaconReceiver.BeaconRangeChangedEvent += BeaconFoundEvent;
		BluetoothState.BluetoothStateChangedEvent += StateChangeEvent;

		// this is untested for Android, but leaving it open to be able to work.
		bool sendingSupported = iBeaconServer.checkTransmissionSupported ();
		if (sendingSupported) {
			InitialiseBluetoothPlugin ();
		}
	}

	public override void StartReceiving(){
		iBeaconReceiver.Scan ();
	}

	public override void StopSending(){
		iBeaconServer.StopTransmit ();
	}

	public override void StopReceiving(){
		iBeaconReceiver.Stop ();
	}

	protected override void SendSignal(Signal s){
		SetServerRegionData (s);
		Diglbug.LogMobile ("RESTARTING "+s.GetSignature()+", "+s.GetPayload() , "BLE_PROC");
		iBeaconServer.Restart ();
		AutoAcceptOwnSignal (s);
	}

	private void AutoAcceptOwnSignal(Signal s){
		FireBeaconFoundEvent (s);
	}

	public override void SetReceiverSignature(Signature s){
		Beacon b = new Beacon(SignalUtils.GetSignaureUUID(s), 0, 0);
		iBeaconReceiver.regions = new iBeaconRegion[]{ new iBeaconRegion (regionName, b) };
		iBeaconReceiver.Restart ();
		Diglbug.LogMobile("RECEIVING "+s+":0,0", "BLE_REC_PROC");
	}

	private void SetServerRegionData(Signal s){
		Diglbug.LogMobile (s.GetSignature()+" : "+s.GetPayload() , "BLE_DATA");
		iBeaconRegion newRegion = new iBeaconRegion(regionName, s.ToBeacon ());
		iBeaconServer.region = newRegion;
	}

	// This is our magic dance to get the bluetooth to initialise properly.
	// Maybe a lazy-loading issue?
	private void InitialiseBluetoothPlugin(){
		Diglbug.LogMobile ("Supported: "+iBeaconServer.checkTransmissionSupported(), "BLE_SUPPORTED");
		Diglbug.LogMobile ("startState: "+BluetoothState.GetBluetoothLEStatus().ToString(), "STATECHANGE_INIT");
	}

	public void StateChangeEvent(BluetoothLowEnergyState state){
		Diglbug.LogMobile ("s:"+state.ToString(), "BLE_STATE");
	}

	public void BeaconFoundEvent(Beacon[] beacons){
		for (int k = 0; k < beacons.Length; k++) {
			int second = System.DateTime.Now.Second;
			Diglbug.LogMobile ((beacons [k].minor-1).ToString()+"@"+second, "REC:"+(beacons [k].major-1).ToString ());
		}

		Signal[] signals = Array.ConvertAll(beacons, item => new Signal(item));
		FireBeaconsFoundEvent (signals);
	}

}