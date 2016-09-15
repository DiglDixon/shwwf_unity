using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
[RequireComponent (typeof(Canvas))]
public class AutoCameraCanvas : MonoBehaviour {

	private void Awake(){
		FindCamera ();
	}

	private void OnEnable(){
		SceneManager.sceneLoaded += FindCameraFunction;
	}

	private void OnDisable(){
		SceneManager.sceneLoaded -= FindCameraFunction;
	}

	private void FindCameraFunction(Scene scene, LoadSceneMode loadMode){
		FindCamera ();
	}

	private void FindCamera(){
		GetComponent<Canvas> ().worldCamera = Camera.main;
	}
}
