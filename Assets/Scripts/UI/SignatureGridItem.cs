using UnityEngine;
using UnityEngine.UI;

public class SignatureGridItem : MonoBehaviour {
	
	public Signature signature = Signature.NONE;
	public Text signatureText;
	public Image signatureImage;

	private SignatureSelector callback;

	// Called from the button
	public void SignatureSelected(){
		callback.SignatureSelected (this);
	}

	public void SetCallback(SignatureSelector callback){
		this.callback = callback;
	}

	public Signature GetSignature(){
		return signature;
	}

	public Sprite SignatureSprite(){
		return (Sprite)signatureImage.sprite;
	}

	private void OnValidate(){
		gameObject.name = signature + "_item";
		signatureText.text = signature.ToString ();
	}

}
