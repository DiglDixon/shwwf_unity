using UnityEngine;
using System.Collections;
using System;

public class BluetoothManager : MonoBehaviour {

	private string regionName = "com.storybox.shwwf";

	public delegate void SignalReceivedDelegate(Signal[] s);
	public event SignalReceivedDelegate SignalReceivedEvent;

	void Start(){
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

	public void StopSending(){
		iBeaconServer.StopTransmit ();
	}

	public void StopReceiving(){
		iBeaconReceiver.Stop ();
	}

	public void SendSignal(Signal s){
		SetServerRegionData (s);
		Diglbug.LogMobile ("RESTARTING "+s.GetSignature()+", "+s.GetPayload() , "BLE_PROC");
		iBeaconServer.Restart ();
	}

	public void SetReceiverSignature(Signature s){
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
		if (SignalReceivedEvent != null)
			SignalReceivedEvent (signals);
	}

}
