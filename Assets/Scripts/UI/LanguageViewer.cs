using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LanguageViewer : MonoBehaviour{

	public Language language = Language.ENGLISH;
	private List<LanguageElement> languageObjects = new List<LanguageElement>();

	public bool refresh = false;

	private void Awake(){
		ReFindObjects();
	}

	private void ReFindObjects(){
		if (SceneManager.GetActiveScene ().isLoaded) {
			GameObject[] allRoots = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().GetRootGameObjects ();
			languageObjects.Clear ();
			for (int k = 0; k < allRoots.Length; k++) {
				LanguageElement[] els = allRoots [k].GetComponentsInChildren<LanguageElement> (true);
				for (int j = 0; j < els.Length; j++) {
					languageObjects.Add (els [j]);
				}
			}
		}

//		languageObjects = transform.parent.gameObject.GetComponentsInChildren <LanguageElement>(true);
	}

//	private void FindObjects(){
//		if (languageObjects == null) {
//			ReFindObjects ();
//		}
//	}

	private void OnValidate(){
		refresh = false;
		ReFindObjects();
		SwitchFoundObjects ();
	}

	private void SwitchFoundObjects(){
		for (int k = 0; k < languageObjects.Count; k++) {
			languageObjects [k].SwitchToLanguage (language);
		}
	}

	public void SetLanguage(Language l){
		Diglbug.Log ("Setting language: " + l);
		language = l;
		ReFindObjects ();
		SwitchFoundObjects ();
		Variables.Instance.SetLanguage (l);
	}

}
