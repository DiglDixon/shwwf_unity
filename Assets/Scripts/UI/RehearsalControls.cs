using UnityEngine;
using System.Collections;

public class RehearsalControls : MonoBehaviour {

	public TracklistPlayer player;
	public ActorPlayer actorPlayer;

	public GameObject playButton;
	public GameObject pauseButton;
	public GameObject unpauseButton;

	public float volumeDuck = 0.5f;
	public AudioSource sourceToDuck;
	private float previousSourceVolume = 1f;

	public void PlayPressed(){
		player.Play ();
	}

	public void StopPressed(){
		actorPlayer.Rehearse_Stop ();
		HideControls ();
	}

	public void OpenControls(){
		previousSourceVolume = sourceToDuck.volume;
		sourceToDuck.volume = volumeDuck;
		gameObject.SetActive (true);
	}

	public void HideControls(){
		sourceToDuck.volume = previousSourceVolume;
		gameObject.SetActive (false);
	}

	public void PausePressed(){
		player.Pause ();
	}

	public void UnpausePressed(){
		player.Unpause ();
	}
	 
	public void NextPressed(){
		actorPlayer.Rehearse_PlayNextAct ();
	}

	public void ClosePressed(){
		HideControls ();
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
