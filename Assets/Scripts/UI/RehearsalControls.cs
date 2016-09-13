using UnityEngine;
using System.Collections;

public class RehearsalControls : MonoBehaviour {

	public TracklistPlayer player;
	public ActorPlayer actorPlayer;

	public GameObject playButton;
	public GameObject pauseButton;
	public GameObject unpauseButton;

	public void PlayPressed(){
		player.Play ();
	}

	public void StopPressed(){
		player.Stop ();
		HideControls ();
	}

	public void HideControls(){
		gameObject.SetActive (false);
	}

	public void PausePressed(){
		player.Pause ();
	}

	public void UnpausePressed(){
		player.Unpause ();
	}

	public void NextPressed(){
		// if we're not on the last one.
		player.PlayNextTrack ();
	}

	private void Update(){
		if (player.IsPlaying ()) {
			SetPlaying ();
		} else if (player.IsPaused ()) {
			SetPaused ();
		} else {
			SetStopped ();
		}
	}

	private void SetPlaying(){
		playButton.SetActive (false);
		pauseButton.SetActive (true);
		unpauseButton.SetActive (false);
	}

	private void SetPaused(){
		playButton.SetActive (false);
		pauseButton.SetActive (false);
		unpauseButton.SetActive (true);
	}

	private void SetStopped(){
		playButton.SetActive (true);
		pauseButton.SetActive (false);
		unpauseButton.SetActive (false);
	}
}
