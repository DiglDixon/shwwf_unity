using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BLE : ConstantSingleton<BLE>{


	public BluetoothManager Manager;

	public delegate void NewSignalFoundDelegate(Signal signal);
	public NewSignalFoundDelegate NewSignalFoundEvent;

	private bool ignoreJockeyProtection = false;

	// parallel arrays - bit nasty, but work for now.
	private Signal currentSignal = SignalUtils.NullSignal;
	private List<float> signalDisableTimes = new List<float>();
	private List<Signal> signalsDisabled = new List<Signal>();

	private float signalDisableTime = 1.5f;

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


//		payloadDisableTimes = new float[Enum.GetNames(typeof(Payload)).Length];
	}

	private bool restrictToSetupSignals = false;

	public void SetupBegins(){
		restrictToSetupSignals = true;
	}

	public void SetupEnds(){
		restrictToSetupSignals = false;
	}

//	private bool IsSetupSignal(Signal s){ TODO: Finish this.
//		Payload p = s.GetPayload ();
//		return (p == Payload.BLUETOOTH_TEST
//			||	p == Payload.STOP_BLUETOOTH_TEST
//			||	p == Payload.FINISH_SETUP
//	}

	public void EnableJockeyProtection(){
		ignoreJockeyProtection = false;
	}

	public void DisableJockeyProtection(){
		ignoreJockeyProtection = true;
	}

	private void Update(){
		float t = Time.deltaTime;
//		Diglbug.Log ("SIZE: " + signalDisableTimes.Count);
		for (int i = signalDisableTimes.Count-1; i >= 0; i--) {
			signalDisableTimes [i] -= t;
//			Diglbug.Log ("Iterating: " + signalsDisabled [i].GetPrint () + " : " + signalDisableTimes [i], PrintStream.DEBUGGING);
			if (signalDisableTimes [i] <= 0f) {
//				if (signalsDisabled [i].Equals (currentSignal)) {
//					Diglbug.Log ("Removing currenSignal " + currentSignal.GetPrint (), PrintStream.SIGNALS);
//					currentSignal = nullSignal;
//				}
				signalDisableTimes.RemoveAt (i);
				signalsDisabled.RemoveAt (i);
			}
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

		if (SignalIsNew (signal)) {
			NewSignalFound (signal);
		}

		ResetDisableTime (signal);
	}

	private void ResetDisableTime(Signal s){
		for (int k = 0; k < signalsDisabled.Count; k++) {
			if (s.Equals (signalsDisabled [k])) {
				signalDisableTimes [k] = signalDisableTime;
				break;
			}
		}
	}

	public void ClearPreviousSignalsFound(){
		Diglbug.Log ("Cleared previous signals found", PrintStream.SIGNALS);
		signalsDisabled.Clear ();
		signalDisableTimes.Clear ();
		currentSignal = SignalUtils.NullSignal;
	}

	private bool SignalIsNew(Signal s){
		if (s.Equals (currentSignal)) {
			return false;
		}
		if (ignoreJockeyProtection) {
			return true;
		}
		for (int k = 0; k < signalsDisabled.Count; k++) {
			if (s.Equals (signalsDisabled [k]))
				return false;
		}
		return true;
	}

	private void NewSignalFound(Signal s){
		if (ShowMode.Instance.IsFabMode ()) {
			return;
		}
		AcceptNewSignal (s);
	}

	private void AcceptNewSignal(Signal s){
		Diglbug.Log ("New signal received: " + s.GetPrint (), PrintStream.SIGNALS);
		Diglbug.LogMobile("UNIQUE: "+s.GetPrint(), "FIRE");
		signalsDisabled.Add (s);
		signalDisableTimes.Add (signalDisableTime);
		currentSignal = s;
		RecoveryManager.Instance.SignalReceived (s);
		//		currentSignal = s;
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

	public void FabModeSignal(Signal s){
		AcceptNewSignal (s);
	}


}