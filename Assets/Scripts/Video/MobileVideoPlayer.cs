using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent (typeof(MediaPlayerCtrl))]
public class MobileVideoPlayer : VideoPlayer {

	public GameObject videoPlane;
	private MediaPlayerCtrl controls;
	private MobileVideoVolumeFader fader;
	private float maxVolume = 1f;

	private void Awake(){
		controls = GetComponent<MediaPlayerCtrl> ();
		fader = new MobileVideoVolumeFader (controls, gameObject);
		fader.AddLerpEndsCallback (FaderLerpEnds);
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
	}

	public override void SetSourceTime(float time){
		base.SetSourceTime (time);
		Diglbug.LogMobile(((int)(time * 1000)).ToString(), "SETVIDSEEK");
		controls.SeekTo((int)(time * 1000));
	}

	public override void Play (){ // need a routine to play if not loaded.
		base.Play ();
		videoPlane.SetActive (true);
		Diglbug.Log ("Play "+name, PrintStream.AUDIO_PLAYBACK);
		Unpause ();
		controls.Play ();
		SetSourceTime(0f);
	}

	public override void Stop(){
		base.Stop ();
		videoPlane.SetActive (false);
		Diglbug.Log ("Stop "+name, PrintStream.AUDIO_PLAYBACK);
		controls.Stop ();
		SetSourceTime(0f);
		fader.CancelFades ();
	}

	public override void Pause(){
		Diglbug.Log ("Pause "+name, PrintStream.AUDIO_PLAYBACK);
		controls.Pause ();
		fader.PauseFades ();
	}

	public override void Unpause(){
		Diglbug.Log ("Unpause "+name, PrintStream.AUDIO_PLAYBACK);
		controls.Play ();
		fader.UnpauseFades ();
	}

	public override bool IsPlaying (){
		return controls.GetCurrentState() == MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING;
	}

	public override void FadeIn(float time){
		Diglbug.Log ("Fade in " + name + ", " + time, PrintStream.AUDIO_PLAYBACK);
		Play ();
		fader.FadeVolumeTo (maxVolume, time);
	}

	public override void FadeOut(float time){
		Diglbug.Log ("Fade out " + name + ", " + time, PrintStream.AUDIO_PLAYBACK);
//		Stop ();
		fader.FadeVolumeTo (0f, time); // this will call Stop() for us
	}

	public override float GetTimeElapsed(){
		if (GetTrack().IsLoaded ()) {
			Diglbug.LogMobile(controls.GetCurrentState().ToString(), "VIDSTATE");
			Diglbug.LogMobile(GetTrack().GetTrackLength().ToString(), "VIDLEN");
			Diglbug.LogMobile((controls.GetSeekPosition() * 0.001f)+"s", "VIDELAPSE");
			return controls.GetSeekPosition () * 0.001f;
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

	protected void FaderLerpEnds(float value){
		if (value == 0f) {
			Stop ();
		}
	}

}
	