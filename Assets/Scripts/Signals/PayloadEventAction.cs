﻿using UnityEngine;

public abstract class PayloadEventAction : MonoBehaviour{

	public abstract void FireEvent(Signal s);

	protected virtual string GetGameObjectName(){
		return this.GetType().ToString()+" event";
	}

	private void UpdateName(){
		gameObject.name = GetGameObjectName ();
	}

	private void OnValidate(){
		UpdateName ();
	}

}
