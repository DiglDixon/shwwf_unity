using UnityEngine;
using UnityEngine.UI;


public class DefinedActText : LanguageElement{


	public DefinedAct act;

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

	public void UpdateValue(DefinedAct a){
		act = a;
		Diglbug.Log ("Switching display signature to: " + act);
		SwitchToLanguage (Variables.Instance.language);
	}

	public override void SwitchToLanguage (Language l){
		FindText ();
		if (l == Language.ENGLISH) {
			if (useSizeOverrides) {
				t.fontSize = overrideEnglishSize;
			}
			t.text = EnumDisplayNamesEnglish.DefinedActName (act);
		} else {
			if (useSizeOverrides) {
				t.fontSize = overrideMandarinSize;
			}
			t.text = EnumDisplayNamesMandarin.DefinedActName (act);
		}
	}


}