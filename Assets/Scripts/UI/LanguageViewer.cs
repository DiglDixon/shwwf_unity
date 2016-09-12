using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LanguageViewer : MonoBehaviour{

	public Language language = Language.ENGLISH;
	private List<LanguageElement> languageObjects = new List<LanguageElement>();

	private void Awake(){
		FindObjects();
	}

	private void ReFindObjects(){
		GameObject[] allRoots = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
		languageObjects.Clear ();
		for (int k = 0; k < allRoots.Length; k++) {
			LanguageElement[] els = allRoots [k].GetComponentsInChildren<LanguageElement> (true);
			for (int j = 0; j < els.Length; j++) {
				languageObjects.Add (els [j]);
			}
		}

//		languageObjects = transform.parent.gameObject.GetComponentsInChildren <LanguageElement>(true);
	}

	private void FindObjects(){
		if (languageObjects == null) {
			ReFindObjects ();
		}
	}

	private void OnValidate(){
		ReFindObjects();
		for (int k = 0; k < languageObjects.Count; k++) {
			languageObjects [k].SwitchToLanguage (language);
		}
	}

}
