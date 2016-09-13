using UnityEngine;


public abstract class LanguageElement : MonoBehaviour{

	public abstract void SwitchToLanguage(Language l);

	private void Start(){
		SwitchToLanguage(Variables.Instance.language);
	}

}