using UnityEngine;
using System.Collections;
using System;

public abstract class BluetoothManager : MonoBehaviour {
	
	protected string regionName = "com.storybox.shwwf";

	private bool expectingSpecificPayload = false;
	private Payload expectedPayload;


	private UnexpectedSignalConfirmation confirmationScreen;

	public delegate void SignalsReceivedDelegate(Signal[] s);
	public event SignalsReceivedDelegate SignalsReceivedEvent;

	public abstract void StopSending ();

	public abstract void StartReceiving ();

	public abstract void StopReceiving ();

	protected virtual void Start(){
		GameObject toolsObject = GameObject.Instantiate (Resources.Load ("BLE_Tools_Canvas")) as GameObject;
		toolsObject.transform.SetParent (transform, false);

		BLETools tools = toolsObject.GetComponent<BLETools> ();

		confirmationScreen = tools.unexpectedSignalConfirmationScreen;
	}

	public void SetExpectedPayload(Payload p){
		Diglbug.Log ("Set expected payload: " + p, PrintStream.SIGNALS);
		expectingSpecificPayload = true;
		expectedPayload = p;
	}

	public bool IsExpectingSpecificPayload(){
		return expectingSpecificPayload;
	}
	// This isn't entirely reliable - make sure to check IsExpectingSpecificPayload first.
	public Payload GetExpectedPayload(){
		return expectedPayload;
	}

	public void ClearExpectedPayload(){
		Diglbug.Log ("Cleared expected payload", PrintStream.SIGNALS);
		expectingSpecificPayload = false;
	}

	public void RequestSignalSend(Signal s){
		if (expectingSpecificPayload) {
			if (s.GetPayload () == expectedPayload) {
				ClearExpectedAndSendSignal (s);
			} else {
				confirmationScreen.OpenWithAttemptedSignal (s);
			}
		} else {
			SendSignal (s);
		}
	}

	public void ForceSignalSend(Signal s){
		ClearExpectedAndSendSignal (s);
	}

	protected void ClearExpectedAndSendSignal(Signal s){
		ClearExpectedPayload ();
		SendSignal (s);
	}

	protected abstract void SendSignal (Signal s);

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
