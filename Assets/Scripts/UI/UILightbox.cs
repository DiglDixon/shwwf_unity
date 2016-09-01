using UnityEngine;
using UnityEngine.UI;


public class UILightbox : MonoBehaviour{

	public void Open(){
		gameObject.SetActive (true);
	}

	public void Close(){
		gameObject.SetActive (false);
	}

}
