using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent (typeof(MediaPlayerCtrl))]
public class MobileVideoPlayer : VideoPlayer {

	public GameObject videoPlane;
	private MediaPlayerCtrl controls;

	private void Awake(){
		controls = GetComponent<MediaPlayerCtrl> ();
	}

	public void InitialiseMobileVideoTracksInList(Tracklist list){
		foreach (TracklistEntry entry in list.entries) {
			if (entry is VideoTracklistEntry) {
				((MobileVideoTrack)entry.GetTrack ()).SetControls (controls);
			}
		}
	}

	public override void SetTrack(ITrack t){
		base.SetTrack (t);
		MobileVideoTrack mobileVideoTrack = (MobileVideoTrack)t;
		Diglbug.Log ("Set Track "+name+", "+mobileVideoTrack.GetTrackName(), PrintStream.VIDEO);

	}

//	public override void SetTrackProgress (float p){
//		SetTrackTime(Mathf.Clamp(p * GetTrack().GetTrackLength(), 0f, GetTrack().GetTrackLength()-0.01f));
//	}
//
//	public override void SetTrackTime(float seconds){
//		Diglbug.Log ("SetTrackProgress "+name+", " + seconds, PrintStream.AUDIO_PLAYBACK);
//		SetSourceTime(seconds);
//	}

	public override void SetSourceTime(float time){
		base.SetSourceTime (time);
		controls.SeekTo((int)(time * 1000));
	}

	public override void Play (){
		videoPlane.SetActive (true);
		Diglbug.Log ("Play "+name, PrintStream.AUDIO_PLAYBACK);
		Unpause ();
		controls.Play ();
		SetSourceTime(0f);
	}

	public override void Stop(){
		videoPlane.SetActive (false);
		Diglbug.Log ("Stop "+name, PrintStream.AUDIO_PLAYBACK);
		controls.Stop ();
		SetSourceTime(0f);
	}

	public override void Pause(){
		Diglbug.Log ("Pause "+name, PrintStream.AUDIO_PLAYBACK);
		controls.Pause ();
	}

	public override void Unpause(){
		Diglbug.Log ("Unpause "+name, PrintStream.AUDIO_PLAYBACK);
		controls.Play ();
	}

	public override bool IsPlaying (){
		return controls.GetCurrentState() == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING;
	}

	public override void FadeIn(float time){
		Diglbug.Log ("Fade in " + name + ", " + time, PrintStream.AUDIO_PLAYBACK);
		Play ();
	}

	public override void FadeOut(float time){
		Diglbug.Log ("Fade out " + name + ", " + time, PrintStream.AUDIO_PLAYBACK);
		Stop ();
	}

	public override float GetTimeElapsed(){
		Diglbug.LogMobile(controls.GetCurrentState().ToString(), "VIDSTATE");
		Diglbug.LogMobile(controls.GetSeekPosition().ToString(), "RAWSEEK");
		Diglbug.LogMobile((controls.GetSeekPosition() * 0.001f)+"s", "VIDSEEK");
		return controls.GetSeekPosition() * 0.001f;
	}

	public override float GetTimeRemaining(){
		return GetTrack().GetTrackLength() - GetTimeElapsed ();
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