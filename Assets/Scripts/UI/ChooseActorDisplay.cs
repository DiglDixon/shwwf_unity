﻿
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChooseActorDisplay : MonoBehaviour {

	public UILightbox actorSelectionLightbox;
	public ActorPlayer actorPlayer;
	private ChooseActorDisplayItem currentItem;

	private Actor pendingActor;

	public void Start(){
		if (!RecoveryManager.Instance.RunningRecovery ()) {
			Open ();
		}
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
		if (actorPlayer.currentActorSet != null) {
			if (pendingActor != actorPlayer.currentActorSet.actor) {
				actorPlayer.SetActor (pendingActor);
			}
		} else {
			actorPlayer.SetActor (pendingActor);
		}
		Close ();
	}

	public void Open(){
		actorSelectionLightbox.Open ();
	}

	public void Close(){
		actorSelectionLightbox.Close ();
	}
}
