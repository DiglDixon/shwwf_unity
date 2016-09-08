
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnsureActorSetsInChildren<T> : MonoBehaviour where T : EnsureActorSetChild {

	public bool update = false;

	private void OnValidate(){
		update = false;

		T[] existing = GetComponentsInChildren<T> ();
		T e = default(T);

		int enumCount = Enum.GetNames (typeof(Actor)).Length;

		for (int k = 0; k < enumCount; k++) {
			e = GetExistingDefinedActsInArray ((Actor)k, existing);
			if(e != null){
				e.UpdateName ();
			}else{
				GameObject newChild = new GameObject ();
				newChild.transform.SetParent (transform);
				T newChildComponent = newChild.AddComponent<T> ();
				newChildComponent.SetEnum((Actor)k);
				newChildComponent.UpdateName();
				newChild.transform.SetSiblingIndex (k);
			}
		}


		for (int k = enumCount; k < existing.Length; k++) {
			existing[k].gameObject.name = "__UNUSED";
		}
	}

	private T GetExistingDefinedActsInArray(Actor actor, T[] array){
		for (int k = 0; k < array.Length; k++) {
			if (array [k].GetEnum() == actor) {
				return array[k];
			}
		}
		return null;
	}

}
