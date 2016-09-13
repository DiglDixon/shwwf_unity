using UnityEngine;

public class IgnoredSignatureDisplay : MonoBehaviour{

	public Signature signature;
	public GameObject whenIgnored;

	public void SetIgnored(bool value){
		whenIgnored.SetActive (value);
	}

	private void OnValidate(){
		name = "ign_"+signature.ToString ();
	}

}