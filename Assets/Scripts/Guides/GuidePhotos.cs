using UnityEngine;
using UnityEngine.UI;

public class GuidePhotos : EnsureDefinedActsInChildren<GuidePhoto>{

	private GuidePhoto[] photos;

	public Sprite defaultSprite;
	public Sprite finalActSprite;

	private void Awake(){
		photos = GetComponentsInChildren<GuidePhoto> ();
	}

	public Sprite GetSpriteForAct(DefinedAct a){
		for (int k = 0; k < photos.Length; k++) {
			if (photos [k].GetDefinedAct () == a) {
				return photos [k].GetPhoto ();
			}
		}
		Diglbug.Log ("Couldn't find Guide Photo for defined act " + a);
		return defaultSprite;
	}

	public Sprite GetFinalActSprite(){
		return finalActSprite;
	}

}