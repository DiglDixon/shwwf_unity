using UnityEngine;

public abstract class EnsureActSetChild : MonoBehaviour{

	public Actor actor;
	public virtual void SetEnum(Actor a){
		actor = a;
	}
	public virtual Actor GetEnum(){
		return actor;
	}

	public void UpdateName(){
		gameObject.name = GetNameString ();
	}

	protected virtual string GetNameString(){
		return GetEnum().ToString ();
	}

	protected virtual void OnValidate(){
		UpdateName ();
	}
}