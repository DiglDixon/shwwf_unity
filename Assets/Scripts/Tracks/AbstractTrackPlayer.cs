using UnityEngine;

public abstract class AbstractTrackPlayer : TrackOutput{


	private float stopMonitorTime = 1f;
	private bool aboutToStop = false;

	private bool paused = false;

	public override void Play(){
		paused = false;
	}

	public override void Stop(){
		paused = false;
	}

	public override void Unpause(){
		paused = false;
	}

	public override void Pause(){
		paused = true;
	}

	public override bool IsPaused(){
		return paused;
	}


	protected virtual void Update(){
		// we assume it's
		if (!IsPlaying () && aboutToStop) {
			Diglbug.Log ("Found rough Stop occurance "+name, PrintStream.AUDIO_PLAYBACK);
			aboutToStop = false;
			TrackReachedEnd ();
		}
		if (IsPlaying ()) {
			aboutToStop = GetTimeRemaining () < stopMonitorTime;
		}
	}

	protected virtual void TrackReachedEnd(){
		Stop ();
	}


	
	public override void SetTrackProgress (float p){
		SetTrackTime(Mathf.Clamp(p * GetTrack().GetTrackLength(), 0f, GetTrack().GetTrackLength()-0.01f));
	}

	public override void SetTrackTime (float seconds){
		Diglbug.Log ("SetTrackTime " + name + ", " + seconds, PrintStream.AUDIO_PLAYBACK);
		SetSourceTime (seconds);
	}

}