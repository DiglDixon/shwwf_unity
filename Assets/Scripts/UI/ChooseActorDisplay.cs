
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChooseActorDisplay : MonoBehaviour {

	public UILightbox actorSelectionLightbox;
	public ActorPlayer actorPlayer;
	private ChooseActorDisplayItem currentItem;

	private Actor pendingActor;

	public void Awake(){
		Open ();
	}

	public void OptionSelected(ChooseActorDisplayItem item){
		if (currentItem != null && item != currentItem) {
			currentItem.Unchosen ();
		}
		currentItem = item;
		currentItem.Chosen ();
		pendingActor = currentItem.actor;
//		actorPlayer.SetActor ();
	}

	public void SelectionConfirmed(){
		actorPlayer.SetActor (pendingActor);
		Close ();
	}

	public void Open(){
		actorSelectionLightbox.Open ();
	}

	public void Close(){
		actorSelectionLightbox.Close ();
	}
}
