using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SignatureSelector : MonoBehaviour {

	public Image selectedSignatureImage;
	public Text selectedSignatureText;

	void Awake(){
		SignatureGridItem[] items = GetComponentsInChildren<SignatureGridItem> ();
		for (int k = 0; k < items.Length; k++) {
			items [k].SetCallback (this);
		}
	}

	public void SignatureSelected(SignatureGridItem selected){
		Signature sig = selected.GetSignature ();

		Diglbug.Log ("Selected Signature " + sig);

		selectedSignatureImage.overrideSprite = selected.SignatureSprite ();
		selectedSignatureText.text = sig.ToString();

		ShowMode.Instance.Signature = sig;

		gameObject.GetComponent<UILightbox> ().Close ();
	}
}
