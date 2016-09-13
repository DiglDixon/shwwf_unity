using UnityEngine;
using UnityEngine.UI;


public class TrackUIControls : MonoBehaviour{

	public TracklistPlayer currentOutput;
	private ITrack currentTrack;

	public TrackSlider trackSlider; 
	public Text trackElapsedText;
	public Text trackLengthText;
	public Text trackNameText;

	public Button playButton;
	public Button pauseButton;
	public Button unpauseButton;
	public Button nextTrackButton;
	public Text nextTrackText;

	public Text otherNotes;
	public GameObject loadingNote;

	private bool scrubbing = false;
	private bool playingBeforeScrub = false;

	private void Update(){
		if (currentOutput != null) {
			if(currentTrack != currentOutput.GetTrack ()){
				ChangeTrackData (currentOutput.GetTrack ());
			}

			if (BLE.Instance.Manager.IsExpectingSpecificPayload ()) {
				otherNotes.text = "Expecting Bluetooth Cue: " + BLE.Instance.Manager.GetExpectedPayload ();
			} else {
				otherNotes.text = "";
			}

			trackSlider.UpdateDisplayValue(currentOutput.GetProgress ());
			UpdateTimeElapsedValue (currentOutput.GetTimeElapsed ());

			loadingNote.SetActive (!currentOutput.GetTrack ().IsLoaded ()); // this is a nasty poll, will remove in the new events system.

			if (currentOutput.IsPlaying ()) {
				SetPlaying ();
			} else if (currentOutput.IsPaused ()) {
				SetPaused ();
			}else{
				SetStopped ();
			}

			if (scrubbing) {
				UpdateTimeElapsedValue (trackSlider.GetValue() * currentOutput.GetTrack().GetTrackLength());
			}
		}
	}

	private void UpdateTimeElapsedValue(float timeElapsed){
		trackElapsedText.text = Utils.AudioTimeFormat (timeElapsed);
	}

	private void ChangeTrackData(ITrack newTrack){
		currentTrack = newTrack;
		trackLengthText.text = Utils.AudioTimeFormat (currentOutput.GetTrack ().GetTrackLength ());
		trackNameText.text = currentOutput.GetTrack ().GetTrackName ();
//		if (newTrack is LoopingTrack) {
//			loopingNote.text = "(Looping)";
//		} else {
//			loopingNote.text = "";
//		}
		SetUpcomingTrackDisplay (currentOutput.GetNextTrack ());
	}

	public void TrackSliderReleased(float v){
		currentOutput.SetTrackProgress (v);
	}

	private void SetNextTrackAvailable(bool v){
		nextTrackButton.interactable = v;
	}

	public void SetUpcomingTrackDisplay(ITrack track){
		if (track != null) {
			nextTrackText.text = "Upcoming track: " + track.GetTrackName ();
			SetNextTrackAvailable (true);
		} else {
			nextTrackText.text = "This is the last track.";
			SetNextTrackAvailable (false);
		}
	}

	public void PauseCurrentOutput(){
		currentOutput.Pause ();
	}

	public void PlayCurrentOutput(){
		currentOutput.Play ();
	}

	public void StopCurrentOutput(){
		currentOutput.Stop ();
	}

	public void UnpauseCurrentOutput(){
		currentOutput.Unpause ();
	}

	public void SkipTrackCurrentOutput(){
		currentOutput.SkipToNextTrack ();
	}

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
		scrubbing = true;
		playingBeforeScrub = currentOutput.IsPlaying ();
		currentOutput.Pause ();
	}

	public void ScrubPositionEnds(float scrubPosition){
		scrubbing = false;
		currentOutput.SetTrackProgress (scrubPosition);
		if (playingBeforeScrub)
			currentOutput.Unpause ();
	}

}


