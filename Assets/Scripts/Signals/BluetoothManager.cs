using UnityEngine;
using System.Collections;
using System;

public abstract class BluetoothManager : MonoBehaviour {
	
	protected string regionName = "com.storybox.shwwf";

	private bool expectingPayload = false;
	private Payload upcomingPayload;

	private Signature sendingSignature;
	private Signature[] receivingSignatures;

	private UnexpectedSignalConfirmation confirmationScreen;

	public delegate void SignalsReceivedDelegate(Signal[] s);
	public event SignalsReceivedDelegate SignalsReceivedEvent;

	public delegate void NewReceivingSignaturesDelegate(Signature[] s);
	public event NewReceivingSignaturesDelegate NewReceivingSignaturesEvent;

	public delegate void NewSendingSignatureDelegate(Signature s);
	public event NewSendingSignatureDelegate NewSendingSignatureEvent;

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

		SetReceivedSignature (Signature.NONE);
		SetSendingSignature (Signature.NONE);
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

	private Signal GetSendingSignalWithPayload(Payload p){
		return new Signal (sendingSignature, p);
	}

	public void RequestPayloadSend(Payload p){
		RequestSendSignal (GetSendingSignalWithPayload(p));
	}

	public void ForceSendPayload(Payload p){
		SendSignal (GetSendingSignalWithPayload(p));
	}

	public void SendExpectedPayload(){
		if (expectingPayload) {
			RequestSendSignal (new Signal (sendingSignature, upcomingPayload));
		} else {
			Diglbug.Log ("Requested SendExpectedPayload when not expecting!");
		}
	}

	public void RequestSendPayload(Payload p){
		RequestSendSignal(GetSendingSignalWithPayload(p));
	}

	private void RequestSendSignal(Signal s){
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

	public virtual void SetReceivedSignature(Signature s){
		SetReceivedSignatures(new Signature[]{s});
	}

	public virtual void SetReceivedSignatures(Signature[] ss){
		receivingSignatures = ss;
		if (NewReceivingSignaturesEvent != null)
			NewReceivingSignaturesEvent (receivingSignatures);
	}

	public virtual void SetSendingSignature(Signature s){
		sendingSignature = s;
		if (NewSendingSignatureEvent != null)
			NewSendingSignatureEvent (sendingSignature);
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
