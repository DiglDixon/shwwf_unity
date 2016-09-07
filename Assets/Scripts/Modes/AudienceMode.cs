using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudienceMode : Mode{

	private Signature signature;

	public Canvas canvas;
	public UILightbox welcomeLightbox;

	public override string ModeName {
		get {
			return "audience";
		}
	}

	public override void BeginShow (){
		SceneManager.LoadScene (Scenes.Setup);
	}

	public override void ModeDeselected (){
		canvas.gameObject.SetActive (false);
		StopCoroutine ("RunBeginShowRoutine");
		BLE.Instance.Manager.StopReceiving ();
	}

	public override void ModeSelected (){
		canvas.gameObject.SetActive (true);
		Diglbug.Log ("Welcome to the show, Audience Member");
		welcomeLightbox.Open ();
		StartCoroutine ("RunBeginShowRoutine");
		BLE.Instance.Manager.StartReceiving ();
	}

	private IEnumerator RunBeginShowRoutine(){
		yield return new WaitForSeconds (1f);
		BeginShow ();
		yield return new WaitForSeconds (1f);
		welcomeLightbox.Close ();
	}

	public void SetSignature(Signature s){
		signature = s;
	}

	public bool AcceptsSignal(Signal s){
		return s.GetSignature() == signature;
	}

	public override void NewSceneLoaded (UnityEngine.SceneManagement.Scene scene){
		Diglbug.Log ("Scene "+scene.name+" begins for " + ModeName, PrintStream.SCENES);
	}


}