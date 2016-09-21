using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudienceMode : Mode{

	public Canvas canvas;
	public UILightbox welcomeLightbox;

	public override ModeName ModeName {
		get {
			return ModeName.AUDIENCE;
		}
	}

	public override void BeginShow (){
		SceneManager.LoadScene (Scenes.MonoScene);
	}

	public override void ModeDeselected (){
		canvas.gameObject.SetActive (false);
		StopCoroutine ("RunBeginShowRoutine");
		BLE.Instance.Manager.StopReceiving ();
	}

	public override void ModeSelected (){
		Diglbug.Log ("Welcome to the show, Audience Member");
		BLE.Instance.Manager.StartReceiving ();
		StartCoroutine ("RunBeginShowRoutine");
//		BeginShow ();
	}

	private IEnumerator RunBeginShowRoutine(){
		canvas.gameObject.SetActive (true);
		welcomeLightbox.Open ();
		yield return new WaitForSeconds (1f);
		BeginShow ();
		yield return new WaitForSeconds (1f);
		welcomeLightbox.Close ();
	}

	public override void NewSceneLoaded (UnityEngine.SceneManagement.Scene scene){
		EnableObjectsWithTag ("Audience");
	}


}