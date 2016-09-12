using UnityEngine;


public class LanguageViewer : MonoBehaviour{

	public Language language = Language.ENGLISH;


	private void OnValidate(){
		LanguageToggleText[] ltts = GameObject.FindObjectsOfType<LanguageToggleText>();
		for (int k = 0; k < ltts.Length; k++) {
			ltts [k].SwitchToLanguage (language);
		}
	}

}
