using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PayloadEventSystem : MonoBehaviour {

	private PayloadEvent[] payloadEvents;

	private void Start(){
		payloadEvents = GetComponentsInChildren<PayloadEvent> ();
		if (payloadEvents.Length < Enum.GetNames (typeof(Payload)).Length) {
			Diglbug.LogError ("Found too few Payload events. This should have been automated. Unsure what can cause this.");
		}

	}
	
	public void HandleNewSignal(Signal s){
		payloadEvents [(int)s.GetPayload ()].FireEvents (s);
	}

	// EDITOR FUNCTIONS__

	public bool update;

	private void OnValidate(){

		update = false;

		PayloadEvent[] existing = GetComponentsInChildren<PayloadEvent> ();
		PayloadEvent e = null;

		int payloadCount = Enum.GetNames (typeof(Payload)).Length;

		for (int k = 0; k < payloadCount; k++) {
			e = GetExistingPayloadInArray ((Payload)k, existing);
			if(e != null){
				// all good, we'll trigger their re-sorting function.
				e.SetPayload(e.payload);
			}else{
				GameObject newChild = new GameObject ();
				newChild.transform.SetParent (transform);
				PayloadEvent newEvent = newChild.AddComponent<PayloadEvent> ();
				newEvent.SetPayload((Payload)k);
			}
		}

		for (int k = payloadCount; k < transform.childCount; k++) {
			transform.GetChild (k).gameObject.name = "__UNUSED";
		}

	}

	private PayloadEvent GetExistingPayloadInArray(Payload payload, PayloadEvent[] array){
		for (int k = 0; k < array.Length; k++) {
			if (array [k].payload == payload) {
				return array[k];
			}
		}
		return null;
	}
}
