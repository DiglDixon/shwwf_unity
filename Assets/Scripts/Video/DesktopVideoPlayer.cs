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

	private float trackTime = 0f;
	private bool trackRunning = false;

	protected override void Update(){
		if (trackRunning) {
			trackTime += Time.deltaTime;
		}
		base.Update ();
	}

	public override void SetTrack(ITrack t){
		DesktopVideoTrack desktopVideoTrack = (DesktopVideoTrack)t;
		Diglbug.Log ("Set Track "+name+", "+desktopVideoTrack.GetTrackName(), PrintStream.AUDIO_PLAYBACK);
		base.SetTrack (desktopVideoTrack);
		audioPlayer.SetTrack (desktopVideoTrack.GetAudioTrack ());
		placeholderImage.material.mainTexture = desktopVideoTrack.GetPlaceholderImage ();
	}

	// These functions are duplicates.
	public override void SetTrackProgress (float p){
		SetTrackTime(Mathf.Clamp(p * GetTrack().GetTrackLength(), 0f, GetTrack().GetTrackLength()-0.01f));
	}

	public override void SetTrackTime(float seconds){
		Diglbug.Log ("SetTrackTime "+name+", " + seconds, PrintStream.AUDIO_PLAYBACK);
		SetSourceTime (seconds);
	}

	public override void SetSourceTime(float time){
		base.SetSourceTime (time);
		trackTime = time;
	}

	public override void Play (){
		base.Play ();
		placeholderScreen.SetActive (true);
		Diglbug.Log ("Play "+name, PrintStream.VIDEO);
		SetSourceTime(0f); 
		Unpause ();
		audioPlayer.Play ();
		trackRunning = true;
	}

	public override void Stop(){
		base.Stop ();
		placeholderScreen.SetActive (false);
		Diglbug.Log ("Stop "+name, PrintStream.VIDEO);
		SetSourceTime(0f);

		audioPlayer.Stop ();
		trackRunning = false;
	}

	public override void Pause(){
		base.Pause ();
		Diglbug.Log ("Pause "+name, PrintStream.VIDEO);

		audioPlayer.Pause ();
		trackRunning = false;
	}

	public override void Unpause(){
		base.Unpause ();
		Diglbug.Log ("Unpause "+name, PrintStream.VIDEO);

		audioPlayer.Unpause ();
		trackRunning = true;
	}

	public override bool IsPlaying (){
		return trackRunning;
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
		Debug.Log ("GTE stakc me");
		return trackTime;
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