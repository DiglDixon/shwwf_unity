using UnityEngine;
using UnityEngine.UI;

public class ChooseActorDisplayItem : LanguageElement{

	public Actor actor;

	public GameObject chosenObject;
	public GameObject unchosenObject;
	public Text nameText;

	public void Chosen(){
		unchosenObject.SetActive (false);
		chosenObject.SetActive (true);
	}

	public void Unchosen(){
		unchosenObject.SetActive (true);
		chosenObject.SetActive (false);
	}

	public override void SwitchToLanguage (Language l){
		string n;
		if (l == Language.ENGLISH) {
			n = EnumDisplayNamesEnglish.ActorName (actor);
		} else {
			n = EnumDisplayNamesMandarin.ActorName (actor);
		}
		nameText.text = n;
		gameObject.name = n;
	}

}
