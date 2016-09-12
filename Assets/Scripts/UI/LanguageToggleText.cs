using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Text))]
public class LanguageToggleText : MonoBehaviour{

	public string mandarinText;
	public string englishText; // this is the default entry

	private Text textToChange;
	public bool updateEnglishToCurrent = false;

	private void Awake(){
		FindText ();

		SwitchToLanguage (Variables.language);
	}

	public void SwitchToLanguage(Language l){
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
	}

}