using UnityEngine;
using System.Collections;
using System;

public abstract class BluetoothManager : MonoBehaviour {
	
	protected string regionName = "com.storybox.shwwf";

	public BLETools bleTools;

	private bool expectingPayload = false;
	private Payload upcomingPayload;

	private Signature sendingSignature;
	private Signature[] receivingSignatures;

	private UnexpectedSignalConfirmation confirmationScreen;

	public delegate void SignalReceivedDelegate(Signal s);
	public event SignalReceivedDelegate SignalReceivedEvent;

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

		confirmationScreen = bleTools.unexpectedSignalConfirmationScreen;

		SetReceivedSignature (Signature.NONE);
		SetSendingSignature (Signature.NONE);

		ClearExpectedPayload ();
	}

	public void SetUpcomingPayload(Payload p){
		Diglbug.Log ("Set expected payload: " + p, PrintStream.SIGNALS);
		upcomingPayload = p;
		if (NewUpcomingPayloadEvent != null) {
			NewUpcomingPayloadEvent (p);
		}
		if (expectingPayload) {
			if (p != GetExpectedPayload ()) {
				Diglbug.Log ("New upcoming payload "+p+"!= expected "+GetExpectedPayload()+". Clearing expected");
				ClearExpectedPayload ();
			}
		}
	}

	public void PayloadExpected(Payload p){
		if (p != upcomingPayload) {
			Diglbug.Log ("Warning: Demanded PayloadExpected "+p+" doesn't match upcomingPayload "+upcomingPayload, PrintStream.SIGNALS);
			SetUpcomingPayload (p);
		}
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

	public void ClearUpcomingPayload(){
		upcomingPayload = Payload.NONE;
	}

	private Signal GetSendingSignalWithPayload(Payload p){
		return new Signal (sendingSignature, p);
	}

	public void ForceSendPayload(Payload p){
		SendSignal (GetSendingSignalWithPayload(p));
	}

	public void SendExpectedPayload(){
		if (expectingPayload) {
			RequestSendSignal (new Signal (sendingSignature, upcomingPayload));
			ClearExpectedPayload ();
		} else {
			Diglbug.Log ("Requested SendExpectedPayload when not expecting!");
		}
	}

	public void RequestSendPayload(Payload p){
		RequestSendSignal(GetSendingSignalWithPayload(p));
	}

	private void RequestSendSignal(Signal s){
		if (upcomingPayload != Payload.NONE){//expectingPayload) {
			if (s.GetPayload () == upcomingPayload) {
				ClearAndSendSignal (s);
			} else {
				Diglbug.LogWarning ("Trying to send unexpected Signal " + s.GetPrint()+", expected: "+upcomingPayload);
				confirmationScreen.OpenWithAttemptedSignal (s);
			}
		} else {
			SendSignal (s);
		}
	}

	public void ForceSignalSend(Signal s){
		ClearAndSendSignal (s);
	}

	protected void ClearAndSendSignal(Signal s){
		ClearExpectedPayload ();
		ClearUpcomingPayload ();
		SendSignal (s);
	}

	protected abstract void SendSignal (Signal s);

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
		for (int k = 0; k < signals.Length; k++) {
			FireBeaconFoundEvent (signals [k]);
		}
	}

	protected void FireBeaconFoundEvent(Signal signal){
		for (int k = 0; k < receivingSignatures.Length; k++) {
			if(receivingSignatures[k] == signal.GetSignature()){
				if (SignalReceivedEvent != null) {
					SignalReceivedEvent (signal);
				}
				continue;
			}
		}
	}

	public void RecoverFromPreviousSignal(Signal s){
		ForceSignalSend (s); // this will keep the time intact
		FireBeaconFoundEvent (s);
	}
}
