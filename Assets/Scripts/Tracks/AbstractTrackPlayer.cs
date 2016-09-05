using UnityEngine;

public abstract class AbstractTrackPlayer : TrackOutput{
	
	public override void SetTrackProgress (float p){
		SetTrackTime(Mathf.Clamp(p * GetTrack().GetTrackLength(), 0f, GetTrack().GetTrackLength()-0.01f));
	}

	public override void SetTrackTime (float seconds){
		Diglbug.Log ("SetTrackTime " + name + ", " + seconds, PrintStream.AUDIO_PLAYBACK);
		SetSourceTime (seconds);
	}

}