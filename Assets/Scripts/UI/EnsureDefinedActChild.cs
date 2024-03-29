﻿using UnityEngine;

public abstract class EnsureDefinedActChild : MonoBehaviour{
	public abstract void SetDefinedAct(DefinedAct a);
	public abstract DefinedAct GetDefinedAct();

	public bool updateName = false;

	public virtual void UpdateName(){
		gameObject.name = GetNameString ();
	}

	protected virtual string GetNameString(){
		return EnumDisplayNamesEnglish.DefinedActName(GetDefinedAct());
	}

	protected virtual void OnValidate(){
		if (updateName) {
			updateName = false;
			UpdateName ();
		}
	}
}