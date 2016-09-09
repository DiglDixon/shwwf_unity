
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChooseActorDisplay : MonoBehaviour {

	public ActorPlayer actorPlayer;
	private ChooseActorDisplayItem currentItem;

	public void OptionSelected(ChooseActorDisplayItem item){
		if (currentItem != null && item != currentItem) {
			currentItem.Unchosen ();
		}
		currentItem = item;
		currentItem.Chosen ();
		actorPlayer.SetActor (currentItem.actor);
	}

	public void Open(){
		gameObject.SetActive (true);
	}

	public void Close(){
		gameObject.SetActive (false);
	}
}
