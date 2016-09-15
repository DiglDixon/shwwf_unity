﻿using UnityEngine;
using System.Collections;
public class DesktopBluetoothManager : BluetoothManager{

	Signal currentSendingSignal = null;
	private float latencyMin = 0.1f;
	private float latencyMax = 1f;
	private bool useLatencySimulation = true;
	private bool holdingLatency = false;

	protected override void SendSignal (Signal s){
		Diglbug.Log ("DT Signal Send");
//		Beacon b = s.ToBeacon ();
		currentSendingSignal = s;
		RecoveryManager.Instance.SignalSent (currentSendingSignal);
		StopCoroutine ("RunFakeReceiving");
		StartCoroutine ("RunFakeReceiving");
	}

	private IEnumerator RunFakeReceiving(){
		if (useLatencySimulation) {
			if (holdingLatency) {
				Diglbug.Log ("Running Holding simulation latency...", PrintStream.SIGNALS);
				while (holdingLatency == true) {
					yield return null;
				}
				Diglbug.Log ("Stopped running simulation latency.", PrintStream.SIGNALS);
			} else {
				float latencySim = GetSimulatedLatency ();
				Diglbug.Log ("Simulating signal latency: " + latencySim, PrintStream.SIGNALS);
				yield return new WaitForSeconds (latencySim);
			}
		}

		while (true) {
			FireBeaconFoundEvent (currentSendingSignal);
			yield return new WaitForSeconds (0.5f);
		}
	}

	private float GetSimulatedLatency(){
		return Random.Range (latencyMin, latencyMax);
	}

	private void Update(){
		if (Input.GetKeyDown (KeyCode.L)) {
			if (useLatencySimulation) {
				holdingLatency = true;
				Diglbug.Log ("Holding latency sim...");
			} else {
				Diglbug.Log ("Not using latency sim.");
			}
		}
		if (Input.GetKeyUp (KeyCode.L)) {
			if (useLatencySimulation) {
				holdingLatency = false;
				Diglbug.Log ("Releasing latency sim...");
			} else {
				Diglbug.Log ("Not using latency sim.");
			}
		}

	}

	/* These are redundant due to our sending sim */
	public override void StopSending (){
		StopCoroutine ("RunFakeReceiving");
	}

	public override void StopReceiving (){
		StopCoroutine ("RunFakeReceiving");
	}

	public override void StartReceiving (){
		//
	}


}