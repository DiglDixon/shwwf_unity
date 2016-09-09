using UnityEngine;
using UnityEngine.UI;

public class ChooseActorDisplayItem : MonoBehaviour{

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

	private void OnValidate(){
		nameText.text = actor.ToString ();
		gameObject.name = actor.ToString ();
	}

}
