using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Text))]
public class LanguageToggleText : LanguageElement{

	[TextArea]
	public string mandarinText;
	public bool updateMandarinToCurrent = false;

	[TextArea]
	public string englishText; // this is the default entry
	public bool updateEnglishToCurrent = false;

	private Text textToChange;

	private void Awake(){
		FindText ();

		SwitchToLanguage (Variables.language);
	}

	public override void SwitchToLanguage(Language l){
		FindText ();
		if (l == Language.MANDARIN) {
			textToChange.text = mandarinText;
		} else if(l == Language.ENGLISH){
			textToChange.text = englishText;
		}
	}

	private void FindText(){
		if (textToChange == null) {
			textToChange = GetComponent<Text> ();
		}
	}

	private void OnValidate(){
		FindText ();
		if (updateEnglishToCurrent) {
			englishText = GetComponent<Text> ().text;
			updateEnglishToCurrent = false;
		}
		if (updateMandarinToCurrent) {
			mandarinText = GetComponent<Text> ().text;
			updateMandarinToCurrent = false;
		}
	}

}