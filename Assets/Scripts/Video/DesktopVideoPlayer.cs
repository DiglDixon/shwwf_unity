using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent (typeof(AudioTrackPlayer))]
public class DesktopVideoPlayer : VideoPlayer {
	
	private AudioTrackPlayer audioPlayer;
	public GameObject placeholderScreen;
	public Image placeholderImage;

	private void Start(){
		audioPlayer = GetComponent<AudioTrackPlayer> ();
	}

	public override void SetTrack(ITrack t){
		DesktopVideoTrack desktopVideoTrack = (DesktopVideoTrack)t;
		Diglbug.Log ("Set Track "+name+", "+desktopVideoTrack.GetTrackName(), PrintStream.AUDIO_PLAYBACK);
		base.SetTrack (desktopVideoTrack);
		audioPlayer.SetTrack (desktopVideoTrack.GetAudioTrack ());
		placeholderImage.material.mainTexture = desktopVideoTrack.GetPlaceholderImage ();
	}

	public override void SetTrackProgress (float p){
		Diglbug.Log ("SetTrackProgress "+name+", " + p, PrintStream.AUDIO_PLAYBACK);
		SetSourceTime(Mathf.Clamp(p * GetTrack().GetTrackLength(), 0f, GetTrack().GetTrackLength()-0.01f));
	}

	public override void SetSourceTime(float time){
		base.SetSourceTime (time);
	}

	public override void Play (){
		placeholderScreen.SetActive (true);
		Diglbug.Log ("Play "+name, PrintStream.VIDEO);
		SetSourceTime(0f); 
		Unpause ();

		audioPlayer.Play ();
	}

	public override void Stop(){
		placeholderScreen.SetActive (false);
		Diglbug.Log ("Stop "+name, PrintStream.VIDEO);
		SetSourceTime(0f);

		audioPlayer.Stop ();
	}

	public override void Pause(){
		Diglbug.Log ("Pause "+name, PrintStream.VIDEO);

		audioPlayer.Pause ();
	}

	public override void Unpause(){
		Diglbug.Log ("Unpause "+name, PrintStream.VIDEO);

		audioPlayer.Unpause ();
	}

	public override bool IsPlaying (){
		return false;
	}

	public override void FadeIn(float time){
		Diglbug.Log ("Fade in " + name + ", " + time, PrintStream.AUDIO_PLAYBACK);
		Play ();
	}

	public override void FadeOut(float time){
		Diglbug.Log ("Fade out " + name + ", " + time, PrintStream.AUDIO_PLAYBACK);
		Stop ();
	}

	// These two aren't ideal - Unity doesn't give us access to the scrub positions of MovieTextures
	// An alternative to this method would be using a parallel coroutine, but this requires a lot of
	// micro-management.
	public override float GetTimeElapsed(){
		return 0f;
	}

	public override float GetTimeRemaining(){
		return GetTrack ().GetTrackLength ();
	}

	public override float GetProgress(){
		return GetTimeElapsed () * GetTrack().GetInverseTrackLength();
	}

	public override void SetMixerGroup(AudioMixerGroup mg){
		// no implementation
	}

}