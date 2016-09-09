﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PayloadEventSystem : EnsurePayloadsInChildren<PayloadEvent> {

	private PayloadEvent[] payloadEvents;
	public delegate void ExternallyDefinedDelegate(Signal s);
	public event ExternallyDefinedDelegate ExternallyDefinedEvent;

	private void Start(){
		payloadEvents = GetComponentsInChildren<PayloadEvent> ();
		if (payloadEvents.Length < Enum.GetNames (typeof(Payload)).Length) {
			Diglbug.LogError ("Found too few Payload events. This should have been automated. Unsure what can cause this.");
		}

	}
	
	public void HandleNewSignal(Signal s){
		payloadEvents [(int)s.GetPayload ()].FireEvents (s);
		if (ExternallyDefinedEvent != null) {
			ExternallyDefinedEvent (s);
		}
	}



}