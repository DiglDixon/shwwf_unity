using UnityEngine;
using UnityEngine.UI;
using System.Collections;
[RequireComponent (typeof(Text))]
public class LoadingText : MonoBehaviour {
	private Text loadingText;
	private int dotCount = 0;
	private WaitForSeconds animationTime = new WaitForSeconds(0.5f);

	private void Awake(){
		loadingText = GetComponent<Text> ();
	}

	private void OnEnable(){
		StartCoroutine ("RunLoadingAnimation");
	}

	private void OnDisable(){
		StopCoroutine ("RunLoadingAnimation");
	}

	private IEnumerator RunLoadingAnimation(){
		while (true) {
			dotCount = (dotCount + 1) % 4;
			loadingText.text = "Loading" + "...".Substring (0, dotCount);
			yield return animationTime;
		}
	}
}
