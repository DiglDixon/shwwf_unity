using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class LanguageSelectButton : MonoBehaviour{

	public Language language;

	public void ButtonPressed(){
		Variables.Instance.SetLanguage (language);
	}

}