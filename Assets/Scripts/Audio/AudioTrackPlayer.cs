using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class AudioTrackPlayer : EventTrackPlayer {
	
	protected AudioSource source;
	private TrackVolumeFader fader;
	private float maxVolume = 1f;

	private void Awake(){
		source = GetComponent<AudioSource> ();
		fader = new TrackVolumeFader (source, gameObject);
		fader.AddLerpEndsCallback (FaderLerpEnds);
	}

	public override void SetMixerGroup(AudioMixerGroup mg){
		source.outputAudioMixerGroup = mg;
	}

	public override void SetTrack(ITrack t){
		base.SetTrack (t);
		AudioTrack audioTrack = (AudioTrack)t;
		Diglbug.Log ("Set Track "+name+", "+audioTrack.GetTrackName(), PrintStream.AUDIO_PLAYBACK);
		source.clip = audioTrack.GetAudioClip();
	}

//	public override void SetTrackProgress (float p){
//		SetTrackTime(Mathf.Clamp(p * GetTrack().GetTrackLength(), 0f, GetTrack().GetTrackLength()-0.01f));
//	}

//	public override void SetTrackTime (float seconds){
//		Diglbug.Log ("SetTrackTime "+name+", " + seconds, PrintStream.AUDIO_PLAYBACK);
//		SetSourceTime(seconds);
//	}

	public override void SetSourceTime(float time){
		base.SetSourceTime (time);
		source.time = time;
	}

	public override void Play (){
		Diglbug.Log ("Play "+name, PrintStream.AUDIO_PLAYBACK);
		SetSourceTime(0f); // these aren't ideal
		Unpause ();
		source.Play ();
		EnableEvents ();
	}

	public override void Stop(){
		Diglbug.Log ("Stop "+name, PrintStream.AUDIO_PLAYBACK);
		fader.CancelFades ();
		source.Stop ();
		SetSourceTime(0f);
		DisableEvents ();
	}

	public override void Pause(){
		Diglbug.Log ("Pause "+name, PrintStream.AUDIO_PLAYBACK);
		fader.PauseFades ();
		source.Pause ();
	}

	public override void Unpause(){
		Diglbug.Log ("Unpause "+name, PrintStream.AUDIO_PLAYBACK);
		fader.UnpauseFades ();
		source.UnPause ();
	}

	public override bool IsPlaying (){
		return source.isPlaying;
	}

	public override void FadeIn(float time){
		Diglbug.Log ("Fade in " + name + ", " + time, PrintStream.AUDIO_PLAYBACK);
		Play ();
		fader.FadeVolumeTo (maxVolume, time);
	}

	public override void FadeOut(float time){
		Diglbug.Log ("Fade out " + name + ", " + time, PrintStream.AUDIO_PLAYBACK);
		fader.FadeVolumeTo (0f, time);
	}

	public override float GetTimeElapsed(){
		return source.timeSamples * GetTrack().GetInverseTrackFrequency();
	}

	public override float GetTimeRemaining(){
		return GetTrack().GetTrackLength() - GetTimeElapsed ();
	}

	public override float GetProgress(){
		return GetTimeElapsed () * GetTrack().GetInverseTrackLength();
	}

	protected void FaderLerpEnds(float value){
		if (value == 0f) {
			Stop ();
		}
	}

}