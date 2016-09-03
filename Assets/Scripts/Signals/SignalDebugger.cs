using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class SignalDebugger : MonoBehaviour{

	public Dropdown signatureDropdown;
	public Dropdown payloadDropdown;

	public Button sendButton;
	public Button stopSendButton;

	public Text receivedSignalsText;

	public Text recDataText;

	public BluetoothManager blueMan;

	void Awake(){
		List<string> options = new List<string>();
		options.AddRange (Enum.GetNames (typeof(Signature)));
		signatureDropdown.AddOptions(options);

		options = new List<string>();
		options.AddRange (Enum.GetNames (typeof(Payload)));
		payloadDropdown.AddOptions(options);

		bool sendSupported = iBeaconServer.checkTransmissionSupported ();
		sendButton.interactable = sendSupported;
		stopSendButton.interactable = sendSupported;

		blueMan.SignalReceivedEvent += SignalsReceivedEvent;
	}

	public void SignalsReceivedEvent(Signal[] signals){
		int minute = System.DateTime.Now.Minute;
		int second = System.DateTime.Now.Second;
		string debugText = "SigRec@ "+minute+":"+second;
		for(int k = 0; k<signals.Length; k++){
			debugText += ", s:"+signals[k].GetSignature()+"p:"+signals[k].GetPayload();
		}
		receivedSignalsText.text = debugText;
	}

	public void SetReceiveDataPressed(){
		Signal s = GetSignalFromDropdowns ();
		recDataText.text = "REC DATA: " + s.GetSignature ();
		blueMan.SetReceiverSignature (s.GetSignature());
	}

	public void SendPressed(){
		blueMan.SendSignal (GetSignalFromDropdowns());
	}

	public void StopSendingPressed(){
		blueMan.StopSending();
	}

	public void StopReceivingPressed(){
		blueMan.StopReceiving();
	}

	private Signal GetSignalFromDropdowns(){
		return new Signal ((Signature)signatureDropdown.value, (Payload)payloadDropdown.value);
	}
}
