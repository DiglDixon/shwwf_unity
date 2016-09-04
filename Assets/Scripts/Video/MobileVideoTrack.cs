using UnityEngine;

public class MobileVideoTrack : VideoTrack{
	
	private MediaPlayerCtrl mobileControls;

	public string videoFileName = "";
	private float inverse_trackLength;
	private float trackLength;

	public void SetControls(MediaPlayerCtrl controls){
		mobileControls = controls;
	}

	public override string GetTrackName(){
		return "m_video_"+videoFileName;
	}

	public override float GetInverseTrackLength(){
		return inverse_trackLength;
	}

	public override float GetInverseTrackFrequency(){
		return 0f; // not used, need to refactor this.
	}

	public override float GetTrackLength(){
		if (mobileControls) {
			return trackLength;
		} else {
			return 1f;
		}
	}

	protected override bool ShouldLoad(){
		return !IsLoaded () && !IsLoading ();
	}

	protected override bool ShouldUnload(){
		MediaPlayerCtrl.MEDIAPLAYER_STATE state = mobileControls.GetCurrentState ();
		return state == MediaPlayerCtrl.MEDIAPLAYER_STATE.STOPPED
			|| state == MediaPlayerCtrl.MEDIAPLAYER_STATE.READY
			|| state == MediaPlayerCtrl.MEDIAPLAYER_STATE.END;
	}

	protected override void RunLoad(){
		mobileControls.Load (videoFileName); // this is where its parameter is set m_str;
		// unfortunately, now is the only time we can call these.
		trackLength = mobileControls.GetDuration ();
		inverse_trackLength = 1f / trackLength;
	}

	protected override void RunUnload(){
		mobileControls.UnLoad ();
	}

	public override bool IsLoaded(){
		MediaPlayerCtrl.MEDIAPLAYER_STATE state = mobileControls.GetCurrentState ();
		return state != MediaPlayerCtrl.MEDIAPLAYER_STATE.NOT_READY
			&& state != MediaPlayerCtrl.MEDIAPLAYER_STATE.ERROR;
	}

	public override bool IsLoading (){
		return false; // don't know where to find this
	}

	public override float FadeTime(){
		return 0f;
	}


}
