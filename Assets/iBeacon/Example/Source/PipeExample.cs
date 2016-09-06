using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


public class PipeExample : MonoBehaviour {
	
	private string s_Region = "com.storybox.twwf16";

	private Beacon digl_beacon = new Beacon(SignalUtils.GetSignaureUUID (Signature.NONE), 0, 0);
	private Beacon digl_receiveBeacon = new Beacon (SignalUtils.GetSignaureUUID (Signature.NONE), 0, 0);


//	private BeaconType bt_PendingType;
//	private BeaconType bt_Type;

	private BroadcastMode bm_Mode;

	// Receive
	private List<Beacon> mybeacons = new List<Beacon>();

	public void RequestBluetoothEnable(){
		BluetoothState.EnableBluetooth();
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

	public void Digl_SetBeacon(Beacon b){
		digl_beacon = b;
		digl_receiveBeacon = new Beacon (b.UUID, 0, 0);;
		// region stays the same.
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
		Debug.Log ("Button StartStop with DiglBeacon " + digl_beacon.UUID + ", " + digl_beacon.minor + ", " + digl_beacon.minor + ", " + s_Region);
		/*** Beacon will start ***/
		if (shouldStart) {
			// ReceiveMode
			if (bm_Mode == BroadcastMode.receive) {
				iBeaconReceiver.BeaconRangeChangedEvent += OnBeaconRangeChanged;
				// check if all mandatory propertis are filled
				if (s_Region == null || s_Region == "") {
					Diglbug.Log ("Null region", PrintStream.SIGNALS);
					return;
				}
				iBeaconReceiver.regions = new iBeaconRegion[]{new iBeaconRegion(s_Region, digl_receiveBeacon)};
				// !!! Bluetooth has to be turned on !!! TODO
				iBeaconReceiver.Scan();
				Debug.Log ("Listening for beacons");
			}
			// SendMode
			else {
				// check if all mandatory propertis are filled
				if (s_Region == null || s_Region == "") {
					Diglbug.Log ("Null region", PrintStream.SIGNALS);
					return;
				}
//				if (s_UUID == null || s_UUID == "") {
//					Diglbug.Log ("Null uuid", PrintStream.SIGNALS);
//					return;
//				}
//
//				if (s_Minor == null || s_Minor == "") {
//					Diglbug.Log ("Null minor", PrintStream.SIGNALS);
//					return;
//				}
				iBeaconServer.region = new iBeaconRegion(s_Region, digl_beacon);
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