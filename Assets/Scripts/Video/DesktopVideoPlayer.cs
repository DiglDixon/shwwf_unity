using UnityEngine;
using UnityEngine.Audio;

[RequireComponent (typeof(AudioTrackPlayer))]
public class DesktopVideoPlayer : VideoPlayer {

	public GameObject videoPlane;
	private MovieTexture movieTexture;
	private AudioTrackPlayer audioPlayer;

	private void Start(){
		audioPlayer = GetComponent<AudioTrackPlayer> ();
	}

	public override void SetTrack(ITrack t){
		DesktopVideoTrack videoTrack = (DesktopVideoTrack)t;
		Diglbug.Log ("Set Track "+name+", "+videoTrack.GetTrackName(), PrintStream.AUDIO_PLAYBACK);
		base.SetTrack (videoTrack);
		// set the plane
		MovieTexture mt = videoTrack.GetVideoTexture();
		videoPlane.GetComponent<Renderer>().material.mainTexture =  mt;
		movieTexture = mt;
		audioPlayer.SetTrack (videoTrack.GetAudioTrack ());
	}

	public override void SetTrackProgress (float p){
		Diglbug.Log ("SetTrackProgress "+name+", " + p, PrintStream.AUDIO_PLAYBACK);
		SetSourceTime(Mathf.Clamp(p * GetTrack().GetTrackLength(), 0f, GetTrack().GetTrackLength()-0.01f));
	}

	public override void SetSourceTime(float time){
		base.SetSourceTime (time);
		// scrubbing not possible with MovieTextures
	}

	public override void Play (){
		videoPlane.SetActive (true);
		Diglbug.Log ("Play "+name, PrintStream.AUDIO_PLAYBACK);
		SetSourceTime(0f); // these aren't ideal
		//		timeAtPause = 0f;
		Unpause ();
		movieTexture.Play ();

		audioPlayer.Play ();
	}

	public override void Stop(){
		videoPlane.SetActive (false);
		Diglbug.Log ("Stop "+name, PrintStream.AUDIO_PLAYBACK);
		movieTexture.Stop ();
		SetSourceTime(0f);

		audioPlayer.Stop ();
	}

	public override void Pause(){
		Diglbug.Log ("Pause "+name, PrintStream.AUDIO_PLAYBACK);
		movieTexture.Pause ();

		audioPlayer.Pause ();
	}

	public override void Unpause(){
		Diglbug.Log ("Unpause "+name, PrintStream.AUDIO_PLAYBACK);
		movieTexture.Play ();

		audioPlayer.Unpause ();
	}

	public override bool IsPlaying (){
		return movieTexture.isPlaying;
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
		if (movieTexture.isPlaying) {
			return 0f;
		} else {
			return GetTrack ().GetTrackLength ();
		}
	}

	public override float GetTimeRemaining(){
		if (movieTexture.isPlaying) {
			return GetTrack ().GetTrackLength () - GetTimeElapsed ();
		} else {
			return 0f;
		}
	}

	public override float GetProgress(){
		return GetTimeElapsed () * GetTrack().GetInverseTrackLength();
	}

	public override void SetMixerGroup(AudioMixerGroup mg){
		// no implementation
	}

}




//// #UNCONFIRMED whether this works or not.
//public bool CallLoaded(){
//	return mediaControls.GetCurrentState () == MediaPlayerCtrl.MEDIAPLAYER_STATE.READY;
//}
//
//// This works. Was developed for the double video, but works for singles too.
//public void LoadCall(string path){
//	mediaControls.Stop ();
//	mediaControls.UnLoad ();
//	mediaControls.Load (path);
//}
//
//public void PlayVideo(){
//	mediaControls.Play ();
//}
//
//// #UNCONFIRMED whether this works at all. Used to incorperate a one-off boolean flip for UNITY_EDITOR.
//public void SeekTo(){
//	mediaControls.SeekTo ((int)((4 * 60) + 16.2f) * 1000);
//}
//
//// this seems dodgey
//public bool CallPlaying(){
//	MediaPlayerCtrl.MEDIAPLAYER_STATE v = mediaControls.GetCurrentState ();
//	return (v != MediaPlayerCtrl.MEDIAPLAYER_STATE.END);
//}
//
//public float GetCallProgress(){
//	return (float) mediaControls.GetCurrentSeekPercent ();
//}
//
//public float GetCallTimeElapsed(){
//	return mediaControls.GetSeekPosition () * 0.001f;
//}