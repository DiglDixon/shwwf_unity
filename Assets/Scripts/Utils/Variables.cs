using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Variables : ConstantSingleton<Variables>{
	public LanguageViewer languageViewer;
	public bool debugBuild = false;

	public Language language;

	public void SetLanguage(Language l){
		language = l;
		languageViewer.SetLanguage (l);
	}
}
