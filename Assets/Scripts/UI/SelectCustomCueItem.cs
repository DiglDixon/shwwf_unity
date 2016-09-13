using UnityEngine.UI;
public class SelectCustomCueItem : EnsureDefinedActChild{

	public DefinedAct act;
	public Text label;
//	public ShowAct act;

	public SelectCustomCueMenu parentMenu;

	public override DefinedAct GetDefinedAct (){
		return act;
	}

	public override void SetDefinedAct (DefinedAct a){
		act = a;
	}

	public override void UpdateName(){
		base.UpdateName ();
		if (label == null) {
			label = GetComponentInChildren<Text> ();
		}
		label.text = act.ToString ();
	}

	public void ButtonPressed(){
		parentMenu.ItemPressed (this);
	}

}