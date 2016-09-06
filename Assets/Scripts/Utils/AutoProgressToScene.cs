using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AutoProgressToScene : MonoBehaviour {

	public GameObject[] objectsToHide;
	public string sceneToLoad;

	// Use this for initialization
	void Start () {
		for (int k = 0; k < objectsToHide.Length; k++) {
			objectsToHide [k].SetActive (false);
		}
		StartCoroutine (RunSceneOpeningRoutine ());
	}

	private IEnumerator RunSceneOpeningRoutine(){
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene (sceneToLoad);
	}
}
