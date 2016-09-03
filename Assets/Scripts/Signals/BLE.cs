using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class BLE : ConstantSingleton<BLE>{

	public BluetoothManager Manager{ get; private set; }

	public delegate void NewSignalFoundDelegate(Signal s);
	public NewSignalFoundDelegate NewSignalFoundEvent;

	private Signal lastSignalReceived;

	private PayloadEventSystem[] eventSystems;


	protected void Start (){
		GameObject managerObject;
		#if UNITY_EDITOR
		managerObject = GameObject.Instantiate(Resources.Load("Desktop_Bluetooth_Manager")) as GameObject;
		#else
		managerObject = GameObject.Instantiate(Resources.Load("Mobile_Bluetooth_Manager")) as GameObject;
		#endif
		managerObject.transform.SetParent(transform);
		Manager = managerObject.GetComponent<BluetoothManager> ();

		lastSignalReceived = SignalUtils.NullSignal;

		Manager.SignalReceivedEvent += ManagerReceivedSignal;


		ReplaceEventSystems ();
		SceneManager.sceneLoaded += FindSceneEventSystems;
	}

	private void FindSceneEventSystems(Scene scene, LoadSceneMode loadMode){
		ReplaceEventSystems ();
	}

	private void ReplaceEventSystems(){
		eventSystems = FindObjectsOfType<PayloadEventSystem> () as PayloadEventSystem[];
		Diglbug.Log ("BLE Loaded scene with eventSystems: " + eventSystems.Length, PrintStream.SIGNALS);
	}

	private void ManagerReceivedSignal(Signal s){
		if (s.GetSignature() != lastSignalReceived.GetSignature () || s.GetPayload () != lastSignalReceived.GetPayload ()) {
			Diglbug.Log ("New signal received: " + s.GetPrint (), PrintStream.SIGNALS);
			if (NewSignalFoundEvent != null) {
				NewSignalFoundEvent (s);
			}
			SendSignalEventToEventSystems (s);
			lastSignalReceived = s;
		}
	}

	private void SendSignalEventToEventSystems(Signal s){
		for (int k = 0; k < eventSystems.Length; k++) {
			eventSystems [k].HandleNewSignal (s);
		}
	}


}