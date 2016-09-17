using UnityEngine;
using UnityEngine.Audio;

public class AudioTrack : EventTrack {

	private float inverse_trackLength;
	private float inverse_trackFrequency;
	private float trackLength;

	[SerializeField]
	private AudioClip audioClip;

	private void Awake(){
		if (audioClip) {
			SetAudioClip (audioClip);
		} else {
			Diglbug.LogWarning ("AudioClip not found at Awake for " + name + ", this must be set before use");
		}
	}

	public void SetAudioClip(AudioClip c){
		audioClip = c;
		trackLength = audioClip.length;
		inverse_trackLength = 1f / trackLength;
		inverse_trackFrequency = 1f / audioClip.frequency;
	}

	public AudioClip GetAudioClip(){
		return audioClip;
	}

	public override string GetTrackName(){
		return audioClip.name;
	}

	public override float GetInverseTrackLength(){
		return inverse_trackLength;
	}

	public override float GetInverseTrackFrequency(){
		return inverse_trackFrequency;
	}

	public override float GetTrackLength(){
		return trackLength;
	}

	protected override bool ShouldLoad(){
//		return (!IsLoaded () && !IsLoading ());
		return false;
	}

	protected override void RunLoad(){
//		audioClip.LoadAudioData ();
	}

	protected override bool ShouldUnload(){
//		return IsLoaded () || IsLoading ();
		return false;
	}

	protected override void RunUnload(){
//		audioClip.UnloadAudioData ();
	}

	public override bool IsLoaded(){
		return true;
//		return audioClip.loadState == AudioDataLoadState.Loaded;
	}

	public override bool IsLoading(){
		return false;
//		return audioClip.loadState == AudioDataLoadState.Loading;
	}

	public override float EntranceFadeTime(){
		return entranceFadeTime;
	}
}
