using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Variables : ConstantSingleton<Variables>{
	public LanguageViewer languageViewer;
	public bool debugBuild = false;

	public Language language;

	private void Start(){
		if (Application.systemLanguage == SystemLanguage.Chinese) {
			SetLanguage (Language.MANDARIN);
		} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified) {
			SetLanguage (Language.MANDARIN);
		} else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
			SetLanguage (Language.MANDARIN);
		} else if (Application.systemLanguage == SystemLanguage.English) {
			SetLanguage (Language.ENGLISH);
		} else {
			SetLanguage (Language.ENGLISH);
		}
	}

	public void SetLanguage(Language l){
		language = l;
		languageViewer.SetLanguage (l);
	}
}
