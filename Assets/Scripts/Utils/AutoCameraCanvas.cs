using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent (typeof(Canvas))]
public class AutoCameraCanvas : MonoBehaviour {

	private void Awake(){
		FindCamera ();
		SceneManager.sceneLoaded += FindCameraFunction;
	}

	private void FindCameraFunction(Scene scene, LoadSceneMode loadMode){
		FindCamera ();
	}

	private void FindCamera(){
		GetComponent<Canvas> ().worldCamera = Camera.main;
	}
}
