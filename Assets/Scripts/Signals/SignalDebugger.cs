﻿using System;
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

	private void Start(){
		List<string> options = new List<string>();
		options.AddRange (Enum.GetNames (typeof(Signature)));
		signatureDropdown.AddOptions(options);

		options = new List<string>();
		options.AddRange (Enum.GetNames (typeof(Payload)));
		payloadDropdown.AddOptions(options);

		bool sendSupported = iBeaconServer.checkTransmissionSupported ();
		sendButton.interactable = sendSupported;
		stopSendButton.interactable = sendSupported;

		BLE.Instance.NewSignalFoundEvent += SignalReceivedEvent;
	}

	public void SignalReceivedEvent(Signal signal){
		int minute = System.DateTime.Now.Minute;
		int second = System.DateTime.Now.Second;
		string debugText = "SigRec@ "+minute+":"+second;
		debugText += ", s:"+signal.GetSignature()+"p:"+signal.GetPayload();
		receivedSignalsText.text = debugText;
	}

	public void SetReceiveDataPressed(){
		Signal s = GetSignalFromDropdowns ();
		recDataText.text = "REC DATA: " + s.GetSignature ();
		BLE.Instance.Manager.SetReceiverSignature (s.GetSignature());
	}

	public void SendPressed(){
		BLE.Instance.Manager.SendSignal (GetSignalFromDropdowns());
	}

	public void StopSendingPressed(){
		BLE.Instance.Manager.StopSending();
	}

	public void StopReceivingPressed(){
		BLE.Instance.Manager.StopReceiving();
	}

	private Signal GetSignalFromDropdowns(){
		return new Signal ((Signature)signatureDropdown.value, (Payload)payloadDropdown.value);
	}
}
