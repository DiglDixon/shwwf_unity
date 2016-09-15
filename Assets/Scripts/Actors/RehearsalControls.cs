using UnityEngine;
using System.Collections;

public class RehearsalControls : MonoBehaviour {

	public TracklistPlayer player;
	public ActorPlayer actorPlayer;
	public ActorDisplays display;

	public GameObject playButton;
	public GameObject pauseButton;
	public GameObject unpauseButton;

	public GameObject rehearsalControlPanel;

	public void PlayPressed(){
//		player.Play ();
		actorPlayer.Rehearse_Play();
//		display.Rehearsel_Begins ();
	}

	public void StopPressed(){
		actorPlayer.Rehearse_Stop ();
		HideControls ();
	}

	public void OpenControls(){
		rehearsalControlPanel.SetActive (true);
	}

	public void HideControls(){
		rehearsalControlPanel.SetActive (false);
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
