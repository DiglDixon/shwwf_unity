using UnityEngine;

public abstract class EnsurePayloadChild : MonoBehaviour{
	public abstract void SetPayload(Payload p);
	public abstract Payload GetPayload();

	public void UpdateName(){
		gameObject.name = GetNameString ();
	}

	protected virtual string GetNameString(){
		return GetPayload().ToString ();
	}

	protected virtual void OnValidate(){
		UpdateName ();
	}
}