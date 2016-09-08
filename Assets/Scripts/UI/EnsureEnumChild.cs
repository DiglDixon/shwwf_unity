//using UnityEngine;
//using System;
//
//public abstract class EnsureEnumChild<E> : MonoBehaviour where E : IConvertible{
//	public abstract void SetEnumValue(E e);
//	public abstract E GetEnumValue();
//
//	public void UpdateName(){
//		gameObject.name = GetNameString ();
//	}
//
//	protected virtual string GetNameString(){
//		return GetEnumValue().ToString ();
//	}
//
//	protected virtual void OnValidate(){
//		UpdateName ();
//	}
//}