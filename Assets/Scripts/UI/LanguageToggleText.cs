using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Text))]
public class LanguageToggleText : LanguageElement{

	[TextArea]
	public string mandarinText;
	public bool updateMandarinToCurrent = false;
	public bool mandarinIsUndefined = false;

	[TextArea]
	public string englishText; // this is the default entry
	public bool updateEnglishToCurrent = false;

	private Text textToChange;

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
		if (mandarinIsUndefined && textToChange.text.Length < 1) {
			Diglbug.LogWarning (name + " found with undefined language text");
			mandarinText = "<UNDEFINED>";
		}
		if (updateEnglishToCurrent) {
			englishText = textToChange.text;
			updateEnglishToCurrent = false;
		}
		if (updateMandarinToCurrent) {
			mandarinText = textToChange.text;
			updateMandarinToCurrent = false;
		}
	}

}