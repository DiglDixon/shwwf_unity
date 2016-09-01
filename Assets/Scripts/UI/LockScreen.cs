using UnityEngine;

public class LockScreen : MonoBehaviour{
	
	public void Open(){
		transform.SetAsLastSibling ();
		gameObject.SetActive (true);
	}

	public void Close(){
		gameObject.SetActive (false);
	}
}
