using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MobileVideoPlayer : VideoPlayer {

	private int controlsIndex;
	private MediaPlayerCtrl[] controls;

	private MobileVideoTrack mobileVideoTrack;

	private void Awake(){
		controls = GetComponentsInChildren<MediaPlayerCtrl> ();
		Diglbug.LogMobile ("ControlCount:" + controls.Length, "ODD");
	}

	public void InitialiseMobileVideoTracksInList(Tracklist list){
		TracklistEntry entry;
		for(int k = 0; k< list.entries.Length; k++) {
			entry = list.entries[k];
			if (entry is VideoTracklistEntry) {
				if (controlsIndex < controls.Length) {
					((MobileVideoTrack)entry.GetTrack ()).SetControls (controls [controlsIndex]);
					Diglbug.LogMobile ("AssignControl:" + controlsIndex, "ODD_2");
					controlsIndex++;
				} else {
					Diglbug.LogError ("Tried to assign a video track with insufficient slots. Please add more Controllers");
				}
			}
		}
	}

	public override void SetTrack(ITrack t){
		base.SetTrack (t);
		mobileVideoTrack = (MobileVideoTrack)t;
	}

	public override void SetSourceTime(float time){
		base.SetSourceTime (time);
		Diglbug.LogMobile(((int)(time * 1000)).ToString(), "SETVIDSEEK");
		mobileVideoTrack.controls.SeekTo((int)(time * 1000));
	}

	public override void Play (){
		if(!mobileVideoTrack.IsLoaded()){ // This is firing every call.
			mobileVideoTrack.Load ();
			StartCoroutine ("RunWhenLoaded");
		}
		base.Play ();
		mobileVideoTrack.controls.ActivatePlane ();
		Diglbug.Log ("Play "+name, PrintStream.AUDIO_PLAYBACK);
		Unpause ();
		mobileVideoTrack.controls.Play ();
		SetSourceTime(0f);
	}

	private IEnumerator RunWhenLoaded(){ // This needs cancelling if no longer asked to play.
		while (!mobileVideoTrack.IsLoaded ()) {
			yield return null;
		}
		Play ();
	}

	public override void Stop(){
		base.Stop ();
		StopCoroutine ("RunWhenLoaded");
		mobileVideoTrack.controls.DeactivatePlane ();
		Diglbug.Log ("Stop "+name, PrintStream.AUDIO_PLAYBACK);
		mobileVideoTrack.controls.Stop ();
		SetSourceTime(0f);
	}

	public override void Pause(){
		base.Pause ();
		Diglbug.Log ("Pause "+name, PrintStream.AUDIO_PLAYBACK);
		mobileVideoTrack.controls.Pause ();
	}

	public override void Unpause(){
		base.Unpause ();
		Diglbug.Log ("Unpause "+name, PrintStream.AUDIO_PLAYBACK);
		mobileVideoTrack.controls.Play ();
	}

	public override bool IsPlaying (){
		if (mobileVideoTrack == null) {
			return false;
		} else {
			return mobileVideoTrack.controls.GetCurrentState () == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING;
		}
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
		if (GetTrack().IsLoaded ()) {
			Diglbug.LogMobile(mobileVideoTrack.controls.GetCurrentState().ToString(), "VIDSTATE");
			Diglbug.LogMobile(GetTrack().GetTrackLength().ToString(), "VIDLEN");
			Diglbug.LogMobile((mobileVideoTrack.controls.GetSeekPosition() * 0.001f)+"s", "VIDELAPSE");
			return mobileVideoTrack.controls.GetSeekPosition () * 0.001f;
		}else{
			return 0f;
		}
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

//	protected void FaderLerpEnds(float value){
//		if (value == 0f) {
//			Stop ();
//		}
//	}

}
	