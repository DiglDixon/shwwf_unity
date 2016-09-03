using UnityEngine;
using System.Collections;
using System;

public abstract class BluetoothManager : MonoBehaviour {
	
	protected string regionName = "com.storybox.shwwf";

	public delegate void SignalsReceivedDelegate(Signal[] s);
	public event SignalsReceivedDelegate SignalsReceivedEvent;

	public abstract void StopSending ();

	public abstract void StartReceiving ();

	public abstract void StopReceiving ();

	public abstract void SendSignal (Signal s);

	public abstract void SetReceiverSignature(Signature s);

	protected void FireBeaconsFoundEvent(Signal[] signals){
		if (SignalsReceivedEvent != null) {
			SignalsReceivedEvent (signals);
		}
	}

	protected void FireBeaconFoundEvent(Signal signal){
		FireBeaconsFoundEvent (new Signal[]{ signal });
	}
}
