using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SignatureSelector : MonoBehaviour {

	public Image[] selectedSignatureImages;
	public TextToSignatureString signatureText;

	void Awake(){
		SignatureGridItem[] items = GetComponentsInChildren<SignatureGridItem> ();
		for (int k = 0; k < items.Length; k++) {
			items [k].SetCallback (this);
		}
	}

	public void SignatureSelected(SignatureGridItem selected){
		Signature sig = selected.GetSignature ();

		Diglbug.Log ("Selected Signature " + sig);

		for (int k = 0; k < selectedSignatureImages.Length; k++) {
			selectedSignatureImages[k].color = selected.SignatureSpriteColour ();
		}
		signatureText.UpdateValue (sig);

		ShowMode.Instance.Signature = sig;

		gameObject.GetComponent<UILightbox> ().Close ();
	}
}
