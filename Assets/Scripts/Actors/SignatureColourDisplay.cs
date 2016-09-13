using UnityEngine;

public class SignatureColourDisplay : MonoBehaviour{

	public Signature signature;
	public GameObject colourObject;

	public void SetColourVisible(bool value){
		colourObject.SetActive (value);
	}

	private void OnValidate(){
		name = "col_"+signature.ToString ();
	}

}