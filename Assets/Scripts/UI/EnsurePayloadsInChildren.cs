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
		T e = default(T);

		int payloadCount = Enum.GetNames (typeof(Payload)).Length;

		for (int k = 0; k < payloadCount; k++) {
			e = GetExistingPayloadInArray ((Payload)k, existing);
			if(e != null){
				e.UpdateName ();
			}else{
				GameObject newChild = new GameObject ();
				newChild.transform.SetParent (transform);
				T newChildComponent = newChild.AddComponent<T> ();
				newChildComponent.SetPayload((Payload)k);
				newChildComponent.UpdateName();
				newChild.transform.SetSiblingIndex (k);
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
