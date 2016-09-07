using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnsurePayloadsInChildren<T> : MonoBehaviour where T : EnsurePayloadChild {

	private T childType;

	public bool update = false;

	private void OnValidate(){
		update = false;

		T[] existing = GetComponentsInChildren<T> ();
		Debug.Log("Found "+existing.Length+" existing children");
		T e = default(T);

		int payloadCount = Enum.GetNames (typeof(Payload)).Length;

		for (int k = 0; k < payloadCount; k++) {
			e = GetExistingPayloadInArray ((Payload)k, existing);
			if(e != null){
				Debug.Log ("Found existing "+((Payload)k)+", all good: " + e.name);
				// all good, we'll trigger their rename
				e.SetPayload(e.GetPayload());
				e.UpdateName();
				e.transform.SetSiblingIndex (k);
			}else{
				GameObject newChild = new GameObject ();
				newChild.transform.SetParent (transform);
				T newChildComponent = newChild.AddComponent<T> ();
				newChildComponent.SetPayload((Payload)k);
				newChildComponent.UpdateName ();
				Debug.Log ("Adding new: " + newChild.name);
			}
		}

		for (int k = payloadCount; k < existing.Length; k++) {
			existing[k].gameObject.name = "__UNUSED";
		}
	}

	private T GetExistingPayloadInArray(Payload payload, T[] array){
		for (int k = 0; k < array.Length; k++) {
			if (array [k].GetPayload() == payload) {
				return array[k];
			}
		}
		return null;
	}

}
