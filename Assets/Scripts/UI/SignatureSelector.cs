using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SignatureSelector : MonoBehaviour {

	public Image[] selectedSignatureImages;
	public TextToSignatureString signatureText;
	private SignatureGridItem[] items;

	public GameObject closeButton;

	public void ForceChange(){
		closeButton.SetActive (false);
		GetComponent<UILightbox> ().Open ();
	}

	void Awake(){
		GatherItems ();
	}

	private void GatherItems(){
		if (items == null) {
			items = GetComponentsInChildren<SignatureGridItem> (true);
			for (int k = 0; k < items.Length; k++) {
				items [k].SetCallback (this);
			}
		}
	}

	public void ResetSignatureDisplays(){
		GatherItems ();
		SignatureGridItem selected = null;
		for (int k = 0; k < items.Length; k++) {
			if (items [k].GetSignature () == ShowMode.Instance.Signature) {
				selected = items[k];
				break;
			}
		}
		if (selected != null) {
			for (int k = 0; k < selectedSignatureImages.Length; k++) {
				selectedSignatureImages [k].color = selected.SignatureSpriteColour ();
			}
			signatureText.UpdateValue (selected.GetSignature ());
		} else {
			// Probably none.
		}

	}

	public void SignatureSelected(SignatureGridItem selected){
		
		Signature sig = selected.GetSignature ();

		Diglbug.Log ("Selected Signature " + sig);

//		for (int k = 0; k < selectedSignatureImages.Length; k++) {
//			selectedSignatureImages[k].color = selected.SignatureSpriteColour ();
//		}
//		signatureText.UpdateValue (sig);

		ShowMode.Instance.Signature = sig;

		ResetSignatureDisplays();

		closeButton.SetActive (true);
		gameObject.GetComponent<UILightbox> ().Close ();
	}
}
