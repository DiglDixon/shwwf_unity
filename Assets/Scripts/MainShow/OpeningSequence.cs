using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpeningSequence : MonoBehaviour {

	public Text countdownText;
	private void OnEnable(){
		StartCoroutine ("RunCountdown");
	}

	private void OnDisable(){
		StopCoroutine ("RunCountdown");
	}

	private IEnumerator RunCountdown(){
		for (int c = 5; c>0; c--){
			countdownText.text = c + "...";
			yield return new WaitForSeconds (0.25f);
		}
		gameObject.SetActive (false);
	}

}
