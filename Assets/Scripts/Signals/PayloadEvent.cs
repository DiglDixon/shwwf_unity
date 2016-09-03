﻿using UnityEngine;
using System.Collections;

public class PayloadEvent : MonoBehaviour {

	public Payload payload;
	private PayloadEventAction[] actions;

	private void Awake(){
		actions = GetComponentsInChildren<PayloadEventAction> ();
	}

	public void SetPayload(Payload p){
		payload = p;
		transform.SetSiblingIndex ((int)payload);
		UpdateName ();
	}

	private void UpdateName(){
		int eventCount = GetComponentsInChildren<PayloadEventAction> ().Length;
		gameObject.name = "("+eventCount+") "+payload.ToString ()+" event";
	}

	private void OnValidate(){
		SetPayload (payload);
	}

	public void FireEvents(Signal s){
		for (int k = 0; k < actions.Length; k++) {
			actions [k].FireEvent (s);
		}
	}

}
