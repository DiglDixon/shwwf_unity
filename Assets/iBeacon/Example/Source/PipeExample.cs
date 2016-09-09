using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


public class PipeExample : MonoBehaviour {
	
	private string digl_region = "com.storybox.twwf16";

	private Beacon digl_sendBeacon = new Beacon(SignalUtils.GetSignaureUUID (Signature.NONE), 0, 0);

	private iBeaconRegion[] digl_receiveRegionArray = new iBeaconRegion[0];

//	private Beacon digl_beacon = new Beacon(SignalUtils.GetSignaureUUID (Signature.NONE), 0, 0);
//	private Beacon digl_receiveBeacon = new Beacon (SignalUtils.GetSignaureUUID (Signature.NONE), 0, 0);

//	private BeaconType bt_PendingType;
//	private BeaconType bt_Type;

	private BroadcastMode bm_Mode;

	// Receive
	private List<Beacon> mybeacons = new List<Beacon>();

	public void RequestBluetoothEnable(){
		BluetoothState.EnableBluetooth();
	}

	private void Awake(){
		digl_receiveRegionArray = new iBeaconRegion[]{
			new iBeaconRegion (digl_region, new Beacon(SignalUtils.GetSignaureUUID(Signature.NONE), 0, 0))
		};
	}

	private void Start() {
		BluetoothState.BluetoothStateChangedEvent += delegate(BluetoothLowEnergyState state) {
			switch (state) {
			case BluetoothLowEnergyState.TURNING_OFF:
			case BluetoothLowEnergyState.TURNING_ON:
				break;
			case BluetoothLowEnergyState.UNKNOWN:
			case BluetoothLowEnergyState.RESETTING:
//				SwitchToStatus();
//				_statusText.text = "Checking Device…";
				break;
			case BluetoothLowEnergyState.UNAUTHORIZED:
//				SwitchToStatus();
//				_statusText.text = "You don't have the permission to use beacons.";
				break;
			case BluetoothLowEnergyState.UNSUPPORTED:
//				SwitchToStatus();
//				_statusText.text = "Your device doesn't support beacons.";
				break;
			case BluetoothLowEnergyState.POWERED_OFF:
//				SwitchToMenu();
//				_bluetoothButton.interactable = true;
//				_bluetoothText.text = "Enable Bluetooth";
				break;
			case BluetoothLowEnergyState.POWERED_ON:
//				SwitchToMenu();
//				_bluetoothButton.interactable = false;
//				_bluetoothText.text = "Bluetooth already enabled";
				break;
			default:
//				SwitchToStatus();
//				_statusText.text = "Unknown Error";
				break;
			}
		};

		BluetoothState.Init();
	}

	public void Digl_SetSendBeacon(Beacon b){
		digl_sendBeacon = b;
	}

	public void Digl_SetReceiveSignature(Signature s){
		Digl_SetReceiveSignatures (new Signature[]{ s });
	}

	public void Digl_SetReceiveSignatures(Signature[] ss){
		Diglbug.Log ("Pipe_SetReceiveSignatures :" + ss.Length);
		digl_receiveRegionArray = new iBeaconRegion[ss.Length];
		for (int k = 0; k < digl_receiveRegionArray.Length; k++) {
			digl_receiveRegionArray[k] = new iBeaconRegion(digl_region, new Beacon(SignalUtils.GetSignaureUUID(ss[k]), 0, 0));
		}
	}

	public void Digl_SetSwitch(BroadcastMode m){
		bm_Mode = m;
	}

	public void Digl_Start(){
		btn_StartStop (true);
	}

	public void Digl_Stop(){
		btn_StartStop (false);
	}

	// BroadcastState
	public void btn_StartStop(bool shouldStart) {
		/*** Beacon will start ***/
		if (shouldStart) {
			// ReceiveMode
			Debug.Log ("Pipe Sending with array length " + digl_receiveRegionArray.Length);
			if (bm_Mode == BroadcastMode.receive) {
				iBeaconReceiver.BeaconRangeChangedEvent += OnBeaconRangeChanged;
				// check if all mandatory propertis are filled
				if (digl_region == null || digl_region == "") {
					Diglbug.Log ("Null region", PrintStream.SIGNALS);
					return;
				}
				iBeaconReceiver.regions = digl_receiveRegionArray;
				// !!! Bluetooth has to be turned on !!! TODO
				iBeaconReceiver.Scan();
				Debug.Log ("Listening for beacons");
			}
			// SendMode
			else {
				Debug.Log ("Pipe Sending with DiglBeacon " + digl_sendBeacon.UUID + ", " + digl_sendBeacon.minor + ", " + digl_sendBeacon.minor + ", " + digl_region);
				// check if all mandatory propertis are filled
				if (digl_region == null || digl_region == "") {
					Diglbug.Log ("Null region", PrintStream.SIGNALS);
					return;
				}

				iBeaconServer.region = new iBeaconRegion(digl_region, digl_sendBeacon);
				// !!! Bluetooth has to be turned on !!! TODO
				iBeaconServer.Transmit();
				Debug.Log ("It is on, go sending");
			}
		} else {
			if (bm_Mode == BroadcastMode.receive) {// Stop for receive
				iBeaconReceiver.Stop();
				iBeaconReceiver.BeaconRangeChangedEvent -= OnBeaconRangeChanged;
			} else { // Stop for send
				iBeaconServer.StopTransmit();
			}
		}
	}

	private void OnBeaconRangeChanged(Beacon[] beacons) { // 
		foreach (Beacon b in beacons) {
			var index = mybeacons.IndexOf(b);
			if (index == -1) {
				mybeacons.Add(b);
			} else {
				mybeacons[index] = b;
			}
		}
		for (int i = mybeacons.Count - 1; i >= 0; --i) {
			if (mybeacons[i].lastSeen.AddSeconds(10) < DateTime.Now) {
				mybeacons.RemoveAt(i);
			}
		}

	}


}