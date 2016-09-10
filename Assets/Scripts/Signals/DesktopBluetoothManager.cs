using UnityEngine;
using System.Collections;
public class DesktopBluetoothManager : BluetoothManager{

	Signal currentSendingSignal = null;
	public float latencyMin = 0.05f;
	public float latencyMax = 4f;
	public bool holdingLatency = false;

	private bool sending = false;

	public override void SendSignal (Signal s){
		Beacon b = s.ToBeacon ();
		currentSendingSignal = new Signal(b);
		StartCoroutine ("RunFakeReceiving");
	}

	private IEnumerator RunFakeReceiving(){
		if (holdingLatency) {
			Diglbug.Log ("Holding simulation latency...", PrintStream.SIGNALS);
			while (holdingLatency) {
				yield return null;
			}
			Diglbug.Log ("Released simulation latency.", PrintStream.SIGNALS);
		} else {
			float latencySim = GetSimulatedLatency();
			Diglbug.Log ("Simulating signal latency: " + latencySim, PrintStream.SIGNALS);
			yield return new WaitForSeconds (latencySim);
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
		holdingLatency = Input.GetKey(KeyCode.L);
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