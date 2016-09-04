using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BLE : ConstantSingleton<BLE>{

	public BluetoothManager Manager{ get; private set; }

	public delegate void NewSignalFoundDelegate(Signal signal);
	public NewSignalFoundDelegate NewSignalFoundEvent;

	private Signal[] lastSignals;

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

		lastSignals = new Signal[0];

		Manager.SignalsReceivedEvent += ManagerReceivedSignals;


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

	private void ManagerReceivedSignals(Signal[] signals){
		Signal s;
		for (int k = 0; k < signals.Length; k++) {
			s = signals [k];
			if(SignalIsNew(s)){
				NewSignalFound (s);
			}
		}
		lastSignals = signals;
	}

	private bool SignalIsNew(Signal s){
		for (int k = 0; k < lastSignals.Length; k++) {
			if (s.Equals (lastSignals [k]))
				return false;
		}
		return true;
	}

	private void NewSignalFound(Signal s){
		Diglbug.Log ("New signal received: " + s.GetPrint (), PrintStream.SIGNALS);
		Diglbug.LogMobile("UNIQUE: "+s.GetPrint(), "FIRE");
		if (NewSignalFoundEvent != null) {
			NewSignalFoundEvent (s);
		}
		SendSignalEventToEventSystems (s);
	}

	private void SendSignalEventToEventSystems(Signal s){
		Diglbug.Log ("Sending signal " + s.GetPrint () + " to " + eventSystems.Length + " systems");
		for (int k = 0; k < eventSystems.Length; k++) {
			eventSystems [k].HandleNewSignal (s);
		}
	}


}