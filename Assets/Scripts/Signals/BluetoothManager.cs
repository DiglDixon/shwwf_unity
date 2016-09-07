using UnityEngine;
using System.Collections;
using System;

public abstract class BluetoothManager : MonoBehaviour {
	
	protected string regionName = "com.storybox.shwwf";

	private bool expectingPayload = false;
	private Payload upcomingPayload;

	private Signature currentSignature;

	private UnexpectedSignalConfirmation confirmationScreen;

	public delegate void SignalsReceivedDelegate(Signal[] s);
	public event SignalsReceivedDelegate SignalsReceivedEvent;

	public delegate void NewSignatureDelegate(Signature s);
	public event NewSignatureDelegate NewSignatureEvent;

	public delegate void NewUpcomingPayloadDelegate(Payload p);
	public event NewUpcomingPayloadDelegate NewUpcomingPayloadEvent;

	public delegate void ExpectedPayloadReadyDelegate(Payload p);
	public event ExpectedPayloadReadyDelegate ExpectedPayloadReadyEvent;

	public delegate void ExpectedPlayloadClearedDelegate();
	public event ExpectedPlayloadClearedDelegate ExpectedPayloadClearedEvent;

	public abstract void StopSending ();

	public abstract void StartReceiving ();

	public abstract void StopReceiving ();

	protected virtual void Start(){
		GameObject toolsObject = GameObject.Instantiate (Resources.Load ("BLE_Tools_Canvas")) as GameObject;
		toolsObject.transform.SetParent (transform, false);

		BLETools tools = toolsObject.GetComponent<BLETools> ();

		confirmationScreen = tools.unexpectedSignalConfirmationScreen;
	}

	public void SetUpcomingPayload(Payload p){
		Diglbug.Log ("Set expected payload: " + p, PrintStream.SIGNALS);
		upcomingPayload = p;
		NewUpcomingPayloadEvent (p);
	}

	public void PayloadExpected(Payload p){
//		if (p != upcomingPayload) {
//			Diglbug.Log ("Warning: Demanded PayloadExpected with a payload that doesn't match upcomingPayload", PrintStream.SIGNALS);
//			SetUpcomingPayload (p);
//		}
		expectingPayload = true;
		if (ExpectedPayloadReadyEvent != null)
			ExpectedPayloadReadyEvent (p);
	}

	public bool IsExpectingSpecificPayload(){
		return expectingPayload;
	}
	// This isn't entirely reliable - make sure to check IsExpectingSpecificPayload first.
	public Payload GetExpectedPayload(){
		return upcomingPayload;
	}

	public void ClearExpectedPayload(){
		Diglbug.Log ("Cleared expected payload", PrintStream.SIGNALS);
		expectingPayload = false;
		upcomingPayload = Payload.NONE;
		if(ExpectedPayloadClearedEvent != null)
			ExpectedPayloadClearedEvent ();
	}

	public void RequestPayloadSend(Payload p){
		RequestSendSignal (new Signal (currentSignature, p));
	}

	public void ForceSendPayload(Payload p){
		SendSignal (new Signal (currentSignature, p));
	}

	public void SendExpectedPayload(){
		if (expectingPayload) {
			RequestSendSignal (new Signal (currentSignature, upcomingPayload));
		} else {
			Diglbug.Log ("Requested SendExpectedPayload when not expecting!");
		}
	}

	public void RequestSendPayload(Payload p){
		RequestSendSignal(new Signal(currentSignature, p));
	}

	public void RequestSendSignal(Signal s){
		if (expectingPayload) {
			if (s.GetPayload () == upcomingPayload) {
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

	public abstract void SendSignal (Signal s);

	public virtual void SetReceiverSignature(Signature s){
		currentSignature = s;
		if (NewSignatureEvent != null)
			NewSignatureEvent (currentSignature);
	}

	protected void FireBeaconsFoundEvent(Signal[] signals){
		if (SignalsReceivedEvent != null) {
			SignalsReceivedEvent (signals);
		}
	}

	protected void FireBeaconFoundEvent(Signal signal){
		FireBeaconsFoundEvent (new Signal[]{ signal });
	}
}
