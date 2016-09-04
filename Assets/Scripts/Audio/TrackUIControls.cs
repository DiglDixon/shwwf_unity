using UnityEngine;
using UnityEngine.UI;


public class TrackUIControls : MonoBehaviour{

	public TrackOutput output;

	public TrackSlider trackSlider; 
	public Text trackElapsedText;
	public Text trackLengthText;
	public Text trackNameText;

	private TrackOutput currentOutput;

	public Button playButton;
	public Button pauseButton;
	public Button unpauseButton;
	public Button nextTrackButton;
	public Text nextTrackText;

	public TracklistControls tracklistControls;

	public Text loopingNote;
	public GameObject loadingNote;

	private bool playingBeforeScrub = false;

	private void Update(){
		if (currentOutput != null) {
			trackSlider.UpdateDisplayValue(currentOutput.GetProgress ());
			trackElapsedText.text =  Utils.AudioTimeFormat (currentOutput.GetTimeElapsed ());
			loadingNote.SetActive (!currentOutput.GetTrack ().IsLoaded ()); // this is a nasty poll, will remove in the new events system.
		}
	}

	public void ChangeTrackData(TrackOutput newOutput){
		currentOutput = newOutput; 
		trackLengthText.text = Utils.AudioTimeFormat (currentOutput.GetTrack ().GetTrackLength ());
		trackNameText.text = currentOutput.GetTrack ().GetTrackName ();
	}

	public void SetLoopingNote(TracklistEntry entry){
		if (entry is LoopingTracklistEntry) {
			LoopingTracklistEntry loopingEntry = (LoopingTracklistEntry)entry;
			loopingNote.text = "Looping, waiting for " + loopingEntry.requiredPayloadToContinue.ToString ()+" cue";
		} else {
			loopingNote.text = "";
		}
	}

	public void TrackSliderReleased(float v){
		currentOutput.SetTrackProgress (v);
	}

	private void SetNextTrackAvailable(bool v){
		nextTrackButton.interactable = v;
	}

	public void SetUpcomingTrackDisplay(TracklistEntry entry){
		if (entry != null) {
			nextTrackText.text = "Upcoming track: " + entry.GetTrack().GetTrackName ();
			SetNextTrackAvailable (true);
		} else {
			nextTrackText.text = "This is the last track.";
			SetNextTrackAvailable (false);
		}
	}

	public void Unpause(){
		SetPlaying();
		output.Unpause ();
	}

	public void Play(){
		SetPlaying();
		output.Play ();
	}

	public void Pause(){
		SetPaused();
		output.Pause ();
	}

	public void Stop(){
		SetStopped();
		output.Stop ();
	}

//	public void NewTrackBegan(){
//		SetPlaying ();
//	}

	private void SetPlaying(){
		pauseButton.gameObject.SetActive (true);
		playButton.gameObject.SetActive (false);
		unpauseButton.gameObject.SetActive (false);
	}

	private void SetPaused(){
		pauseButton.gameObject.SetActive (false);
		playButton.gameObject.SetActive (false);
		unpauseButton.gameObject.SetActive (true);
	}

	private void SetStopped(){
		pauseButton.gameObject.SetActive (false);
		playButton.gameObject.SetActive (true);
		unpauseButton.gameObject.SetActive (false);
	}

	public void ScrubPositionBegins(){
		playingBeforeScrub = output.IsPlaying ();
		output.Pause ();
	}

	public void ScrubPositionEnds(float scrubPosition){
		output.SetTrackProgress (scrubPosition);
		if (playingBeforeScrub)
			output.Unpause ();
	}

}


