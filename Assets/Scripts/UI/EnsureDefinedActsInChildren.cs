using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnsureDefinedActsInChildren<T> : MonoBehaviour where T : EnsureDefinedActChild {

	public bool update = false;
	public GameObject customInstantiatedObject;

	protected virtual void OnValidate(){
		update = false;

		T[] existing = GetComponentsInChildren<T> ();
		T e = default(T);

		int enumCount = Enum.GetNames (typeof(DefinedAct)).Length;

		for (int k = 0; k < enumCount; k++) {
			e = GetExistingDefinedActsInArray ((DefinedAct)k, existing);
			if(e != null){
				e.UpdateName ();
			}else{
				GameObject newChild;
				if (customInstantiatedObject != null) {
					newChild = GameObject.Instantiate (customInstantiatedObject) as GameObject;
				} else {
					newChild = new GameObject ();
				}
				newChild.transform.SetParent (transform, false);
				T newChildComponent = newChild.GetComponent<T> ();
				if(newChildComponent == null){
					newChildComponent = newChild.AddComponent<T> ();
				}
				newChildComponent.SetDefinedAct((DefinedAct)k);
				newChildComponent.UpdateName();
				newChild.transform.SetSiblingIndex (k);
				RunExtraInitsToObject (newChild);
			}
		}




		for (int k = enumCount; k < existing.Length; k++) {
			existing[k].gameObject.name = "__UNUSED";
		}
	}

	private T GetExistingDefinedActsInArray(DefinedAct definedAct, T[] array){
		for (int k = 0; k < array.Length; k++) {
			if (array [k].GetDefinedAct() == definedAct) {
				return array[k];
			}
		}
		return null;
	}

	protected virtual void RunExtraInitsToObject(GameObject newObject){
		//
	}

}
