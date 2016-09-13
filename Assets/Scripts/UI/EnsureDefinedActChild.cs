using UnityEngine;

public abstract class EnsureDefinedActChild : MonoBehaviour{
	public abstract void SetDefinedAct(DefinedAct a);
	public abstract DefinedAct GetDefinedAct();

	public virtual void UpdateName(){
		gameObject.name = GetNameString ();
	}

	protected virtual string GetNameString(){
		return GetDefinedAct().ToString ();
	}

	protected virtual void OnValidate(){
		UpdateName ();
	}
}