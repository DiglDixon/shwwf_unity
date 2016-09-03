using UnityEngine;
using System.Collections;
using System;

public abstract class BluetoothManager : MonoBehaviour {
	
	protected string regionName = "com.storybox.shwwf";

	public delegate void SignalReceivedDelegate(Signal s);
	public event SignalReceivedDelegate SignalReceivedEvent;

	public abstract void StopSending ();

	public abstract void StartReceiving ();

	public abstract void StopReceiving ();

	public abstract void SendSignal (Signal s);

	public abstract void SetReceiverSignature(Signature s);

	protected void FireBeaconFoundEvent(Signal signal){
		Diglbug.Log ("FireBeaconFoundEvent "+signal.GetSignature()+":"+signal.GetPayload(), PrintStream.SIGNALS);
		if (SignalReceivedEvent != null) {
			SignalReceivedEvent (signal);
		}
	}

	protected void FireBeaconsFoundEvent(Signal[] signals){
		for (int k = 0; k < signals.Length; k++) {
			FireBeaconFoundEvent (signals [k]);
		}
	}
}
