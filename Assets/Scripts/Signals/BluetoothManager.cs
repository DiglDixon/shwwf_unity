using UnityEngine;
using System.Collections;

public class BluetoothManager : MonoBehaviour {

	private bool blePluginReady = false;
	private WaitForSeconds blePluginReadyPollTime = new WaitForSeconds(0.5f);
	
	private void Start(){
		#if UNITY_IOS
		BluetoothState.BluetoothStateChangedEvent += StateChange;
		InitialiseBluetoothPlugin();
		StartCoroutine(SendWhenReady());
		#endif

//		#if UNITY_ANDROID
//		iBeaconReceiver.Scan();
//		iBeaconReceiver.BeaconRangeChangedEvent += BeaconEvent;
//		Diglbug.LogMobile("RECEIVING", "BLE_STATE");
//		#endif
	}

	private IEnumerator SendMultipleMinors(){
		int minorCount = 1;
		while (true) {
//			iBeaconServer.
			minorCount++;

			Diglbug.LogMobile ("RESTARTING...", "BLE_PROC");
			iBeaconServer.region = new iBeaconRegion("com.storybox.shwwf", new Beacon(iBeaconServer.region.beacon.UUID, 12345, minorCount));
			iBeaconServer.Restart ();
			yield return new WaitForSeconds (5f);
		}
	}

	// This is our magic dance to get the bluetooth to initialise properly.
	// Maybe a lazy-loading issue?
	private void InitialiseBluetoothPlugin(){
		Diglbug.LogMobile ("Supported: "+iBeaconServer.checkTransmissionSupported(), "BLE_SUPPORTED");
		Diglbug.LogMobile ("startState: "+BluetoothState.GetBluetoothLEStatus().ToString(), "STATECHANGE_INIT");
	}

	private IEnumerator SendWhenReady(){
		Diglbug.LogMobile ("WAITING...", "BLE_PROC");
		while (!blePluginReady) {
			yield return blePluginReadyPollTime;
		}
		Diglbug.LogMobile ("SENDING", "BLE_PROC");
		iBeaconServer.Transmit ();
		yield return new WaitForSeconds (1f);
		StartCoroutine (SendMultipleMinors ());
	}

	int stateChangeCount = 0;
	public void StateChange(BluetoothLowEnergyState state){
		Diglbug.LogMobile ("s:"+state.ToString(), "BLE_STATE");
		if (state == BluetoothLowEnergyState.POWERED_ON) {
			blePluginReady = true;
		} else {
			blePluginReady = false;
		}
		stateChangeCount++;
	}

	public void BeaconEvent(Beacon[] beacons){
		for (int k = 0; k < beacons.Length; k++) {
			Diglbug.LogMobile (beacons [k].minor.ToString(), beacons [k].major.ToString ());
		}
	}

//	public void SendSignal(){
//	}

//	iBeaconReceiver.regions = new iBeaconRegion[]{new iBeaconRegion(s_Region, new Beacon(s_UUID, Convert.ToInt32(s_Major), Convert.ToInt32(s_Minor)))};


}
