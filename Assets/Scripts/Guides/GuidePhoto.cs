using UnityEngine;
using UnityEngine.UI;
public class GuidePhoto : EnsureDefinedActChild{
	public DefinedAct actToStart;

	public Sprite sprite;

	public override DefinedAct GetDefinedAct (){
		return actToStart;
	}

	public override void SetDefinedAct (DefinedAct a){
		actToStart = a;
	}

	public Sprite GetPhoto(){
		return sprite;
	}
}