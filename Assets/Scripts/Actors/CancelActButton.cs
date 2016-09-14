using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Button))]
public class CancelActButton : MonoBehaviour {

	public GameObject[] toEnableWhenActing;
	public GameObject[] toEnableWhenNotActing;

	private Button button;
	public ActorPlayer actorPlayer;

	private void Awake () {
		button = GetComponent<Button> ();
		actorPlayer.ActingToNewGroupEvent += NewActingBegan;
	}

	private void OnEnable(){
//		actorPlayer.ActingToNewGroupEvent += NewActingBegan;
	}

	private void OnDisable(){
//		actorPlayer.ActingToNewGroupEvent -= NewActingBegan;
	}

	private void NewActingBegan(Signature s){
		button.interactable = (s != Signature.NONE);
		if (s == Signature.NONE) {
			for(int k = 0; k<toEnableWhenActing.Length; k++){
				toEnableWhenActing [k].SetActive (false);
			}

			for(int k = 0; k<toEnableWhenNotActing.Length; k++){
				toEnableWhenNotActing [k].SetActive (true);
			}
		} else {
			for(int k = 0; k<toEnableWhenActing.Length; k++){
				toEnableWhenActing [k].SetActive (true);
			}

			for(int k = 0; k<toEnableWhenNotActing.Length; k++){
				toEnableWhenNotActing [k].SetActive (false);
			}
		}
	}
}
