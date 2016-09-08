//using UnityEngine;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//
//public class EnsureEnumsInChildren<T, E> : MonoBehaviour where E : IConvertible where T : EnsureEnumChild<E>  {
//
//	public bool update = false;
//
//	private void OnValidate(){
//		update = false;
//
//		T[] existing = GetComponentsInChildren<T> ();
//		T e = default(T);
//
//		int enumCount = Enum.GetNames (typeof(E)).Length;
//		Enum EnumType = (E)E;
//		for (int k = 0; k < enumCount; k++) {
//			e = GetExistingEnumChildInArray ((Enum.GetValues(E))[k], existing);
//			if(e != null){
//				e.UpdateName ();
//			}else{
//				GameObject newChild = new GameObject ();
//				newChild.transform.SetParent (transform);
//				T newChildComponent = newChild.AddComponent<T> ();
//				newChildComponent.SetEnumValue((EnumType)k);
//				newChildComponent.UpdateName();
//				newChild.transform.SetSiblingIndex (k);
//			}
//		}
//
//
//		for (int k = enumCount; k < existing.Length; k++) {
//			existing[k].gameObject.name = "__UNUSED";
//		}
//	}
//
//	private T GetExistingEnumChildInArray(E enumValue, T[] array){
//		for (int k = 0; k < array.Length; k++) {
//			if (array [k].GetEnumValue().Equals(enumValue)) {
//				return array[k];
//			}
//		}
//		return null;
//	}
//
//}
