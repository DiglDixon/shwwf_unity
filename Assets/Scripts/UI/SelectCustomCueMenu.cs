using UnityEngine;


public class SelectCustomCueMenu : EnsureDefinedActsInChildren<SelectCustomCueItem>{
	// concrete

	public bool disableRecreation = true;

	public ActSet actPayloadPairs;

	public DefinedActText confirmText;

	private SelectCustomCueItem pendingItem;
	public UILightbox startCustomSceneConfirmationLightbox;

	public SceneControls sceneControls;

	public bool requiresConfirmation = true;

	public void ItemPressed(SelectCustomCueItem item){
		// do stuff
		pendingItem = item;
		if (requiresConfirmation) {
			confirmText.UpdateValue (item.act);
			startCustomSceneConfirmationLightbox.Open ();
			Diglbug.Log ("Custom act requested select " + item.act);
		} else {
			CustomSceneConfirmed ();
		}
	}

	public void CustomSceneConfirmed(){
		if (pendingItem == false) {
			Diglbug.LogError ("Confirmed an undefined custom scene start.");
			return;
		}
		sceneControls.BeginCustomAct (actPayloadPairs.GetActForDefinedAct(pendingItem.act));
		Diglbug.Log ("Confirmed: Sending custom act from item select " + pendingItem.act);
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
