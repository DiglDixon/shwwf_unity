using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestVideoMethods : MonoBehaviour {

	public MediaPlayerCtrl controls;
	public Text stateText;

	public void LoadButtonPressed(){
		controls.Load ("EasyMovieTexture.mp4");
	}

	public void Seek(float seconds){
		controls.SeekTo ((int)(seconds * 1000f));
	}

	public void PlayButtonPressed(){
		controls.Play ();
	}

	public void StopButtonPressed(){
		controls.Stop ();
	}

	void Update(){
		stateText.text = controls.GetCurrentState ().ToString();
	}

}
