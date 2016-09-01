using UnityEngine;
using UnityEngine.Audio;

public class AudioTrack : EventTrack {

	private float inverse_trackLength;
	private float inverse_trackFrequency;
	private float trackLength;
	[SerializeField]
	private float selfFadeTime = 1f;

	[SerializeField]
	private AudioClip audioClip;

	private void Awake(){
		if (!audioClip)
			throw new UnassignedReferenceException ("Track missing AudioClip at Awake");
		SetAudioClip (audioClip);
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

	public override void Load(){
		Diglbug.Log ("Loading track " + audioClip.name, PrintStream.AUDIO_LOAD);
		audioClip.LoadAudioData ();
	}

	public override void Unload(){
		Diglbug.Log ("Unloading track " + audioClip.name, PrintStream.AUDIO_LOAD);
		audioClip.UnloadAudioData ();
	}

	public override bool IsLoaded(){
		return audioClip.loadState.Equals (AudioDataLoadState.Loaded);
	}

	public override float FadeTime(){
		return selfFadeTime;
	}

	private void OnValidate()
	{
		gameObject.name = GetTrackName ();
			
	}
}
