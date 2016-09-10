using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BLE : ConstantSingleton<BLE>{


	public BluetoothManager Manager;

	public delegate void NewSignalFoundDelegate(Signal signal);
	public NewSignalFoundDelegate NewSignalFoundEvent;

	private float[] payloadDisableTimes;
	private float payloadDisableTime = 1.5f;

	private PayloadEventSystem[] eventSystems;

	protected override void Awake (){
		base.Awake ();
		GameObject managerObject;
		#if UNITY_EDITOR
		managerObject = GameObject.Instantiate(Resources.Load("Desktop_Bluetooth_Manager")) as GameObject;
		managerObject.transform.SetParent(transform);
		Manager = managerObject.GetComponent<BluetoothManager> ();
		#endif
//		#else
//		managerObject = GameObject.Instantiate(Resources.Load("Mobile_Bluetooth_Manager")) as GameObject;
//		#endif

//		lastSignals = new Signal[0];

		Manager.SignalReceivedEvent += ManagerReceivedSignal;


		ReplaceEventSystems ();
		SceneManager.sceneLoaded += FindSceneEventSystems;


		payloadDisableTimes = new float[Enum.GetNames(typeof(Payload)).Length];
	}

	private void Update(){
		float t = Time.deltaTime;
		for (int k = 0; k < payloadDisableTimes.Length; k++) {
			payloadDisableTimes [k] = Mathf.Max (payloadDisableTimes [k] - t, 0f);
		}
	}

	private void FindSceneEventSystems(Scene scene, LoadSceneMode loadMode){
		ReplaceEventSystems ();
	}

	private void ReplaceEventSystems(){
		eventSystems = FindObjectsOfType<PayloadEventSystem> () as PayloadEventSystem[];
		Diglbug.Log ("BLE Loaded scene with eventSystems: " + eventSystems.Length, PrintStream.SIGNALS);
	}

	private void ManagerReceivedSignal(Signal signal){

		if(SignalIsNew(signal)){
			NewSignalFound (signal);
		}

		ResetDisableTime (signal.GetPayload ());
	}

	private void ResetDisableTime(Payload p){
		payloadDisableTimes [(int)p] = payloadDisableTime;
	}

	private bool SignalIsNew(Signal s){
//		for (int k = 0; k < lastSignals.Length; k++) {
//			if (s.Equals (lastSignals [k]))
//				return false;
//		}
//		return true;
		return payloadDisableTimes [(int)s.GetPayload ()] == 0f;

//		}
//		if (lastSignal == null) {
//			return true;
//		} else {
//			return s.Equals (lastSignal);
//		}
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