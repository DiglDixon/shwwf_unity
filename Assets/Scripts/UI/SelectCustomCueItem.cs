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
		string actNumber = ((int)act)+".";
		if (ShowMode.Instance.Mode.ModeName == ModeName.GUIDE) {
			if (Variables.Instance.language == Language.ENGLISH) {
				label.text = actNumber+" "+EnumDisplayNamesEnglish.DefinedActName (act);
			} else {
				label.text = actNumber+" "+EnumDisplayNamesMandarin.DefinedActName (act);
			}
		} else {
			label.text = actNumber;
		}
	}

	public void ButtonPressed(){
		parentMenu.ItemPressed (this);
	}

}