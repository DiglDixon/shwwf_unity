using UnityEngine;

public class EmptyTrack : ITrack{
	
	public EmptyTrack (){
	}

	public string GetTrackName(){
		return "emptyTrack";
	}

	public float GetInverseTrackLength(){
		return .033333333f;
	}

	public float GetInverseTrackFrequency(){
		return 1f;
	}

	public float GetTrackLength(){
		return 30f;
	}

	public bool IsLoaded(){
		return false;
	}

	public bool IsLoading(){
		return false;
	}

	public void Load(){
		//
	}

	public void Unload(){
		//
	}

	public void SkipToProgress(float f){

	}

	public float EntranceFadeTime(){
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

