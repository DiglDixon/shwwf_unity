﻿using UnityEngine;


public class SelectCustomCueMenu : EnsureDefinedActsInChildren<SelectCustomCueItem>{
	// concrete

	public bool disableRecreation = true;

	public ActSet actPayloadPairs;

	public DefinedActText confirmText;

	private SelectCustomCueItem pendingItem;
	public UILightbox startCustomSceneConfirmationLightbox;

	public GuideControls guideControls;

	public void ItemPressed(SelectCustomCueItem item){
		// do stuff
		pendingItem = item;
		confirmText.UpdateValue (item.act);
		startCustomSceneConfirmationLightbox.Open ();
		Diglbug.Log ("Custom act requested select " + item.act);
	}

	public void CustomSceneConfirmed(){
		if (pendingItem == false) {
			Diglbug.LogError ("Confirmed an undefined custom scene start.");
			return;
		}
		Payload toSend = actPayloadPairs.GetPayloadForDefinedAct(pendingItem.act);
		Diglbug.Log ("Confirmed: Sending custom cue " + toSend + " from item select " + pendingItem.act);
		guideControls.BeginCustomScene (toSend);
	}

	protected override void RunExtraInitsToObject (GameObject newObject){
		newObject.GetComponent<SelectCustomCueItem>().parentMenu = this;
	}

	protected override void OnValidate (){
		if (disableRecreation == false) {
			base.OnValidate ();
		}
	}

}
