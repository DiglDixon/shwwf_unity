using UnityEngine;

public class EmptyTrack : ITrack{
	
	public EmptyTrack (){
	}

	public string GetTrackName(){
		return "emptyTrack";
	}

	public float GetInverseTrackLength(){
		return 1f;
	}

	public float GetInverseTrackFrequency(){
		return 1f;
	}

	public float GetTrackLength(){
		return 0f;
	}

	public bool IsLoaded(){
		return true;
	}

	public void Load(){
		//
	}

	public void Unload(){
		//
	}

	public void SkipToProgress(float f){

	}

	public float FadeTime(){
		return 0;
	}

	public AudioClip GetAudioClip(){
		return null;
	}

	public void UpdateTimeElapsed(float newTime){
		//
	}

	public void SetTimeElapsed(float newTime){
		//
	}
}

