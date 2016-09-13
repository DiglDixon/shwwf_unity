using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Text))]
public class TextToSignatureString : LanguageElement {

	public Signature signature;

	public bool useSizeOverrides = false;
	public int overrideMandarinSize = 0;
	public int overrideEnglishSize = 0;

	public string englishNoneValue;
	public string mandarinNoneValue;

	private Text t;

	void Awake(){
		FindText ();
		SwitchToLanguage (Variables.Instance.language);
	}

	private void FindText(){
		if (t == null) {
			t = GetComponent<Text> ();
		}
	}

	public void UpdateValue(Signature s){
		signature = s;
		Diglbug.Log ("Switching display signature to: " + s);
		SwitchToLanguage (Variables.Instance.language);
	}

	public override void SwitchToLanguage (Language l){
		FindText ();
		if (l == Language.ENGLISH) {
			if (useSizeOverrides) {
				t.fontSize = overrideEnglishSize;
				if (signature == Signature.NONE) {
					t.text = englishNoneValue;
				} else {
					t.text = EnumDisplayNamesEnglish.SignatureName (signature);
				}
			}
		} else {
			if (useSizeOverrides) {
				t.fontSize = overrideMandarinSize;
				if (signature == Signature.NONE) {
					t.text = mandarinNoneValue;
				} else {
					t.text = EnumDisplayNamesMandarin.SignatureName (signature);
				}
			}
		}
	}


}
