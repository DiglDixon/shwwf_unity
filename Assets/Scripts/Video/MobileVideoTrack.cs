using UnityEngine;

public class MobileVideoTrack : VideoTrack{
	
	private MediaPlayerCtrl mobileControls;
	public float manualVideoLength;
	public string videoFileName = "";
	private float inverse_trackLength;
	private float trackLength;

	private void Awake(){
		// We set this manually, as it can't be found until the track is loaded.
		trackLength = manualVideoLength;
		inverse_trackLength = 1f / trackLength;
	}

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
		return trackLength;
	}

	protected override bool ShouldLoad(){
		return !IsLoaded () && !IsLoading ();
	}

	protected override bool ShouldUnload(){
		MediaPlayerCtrl.MEDIAPLAYER_STATE state = mobileControls.GetCurrentState ();
		return state == MediaPlayerCtrl.MEDIAPLAYER_STATE.STOPPED
			|| state == MediaPlayerCtrl.MEDIAPLAYER_STATE.READY
			|| state == MediaPlayerCtrl.MEDIAPLAYER_STATE.PAUSED
			|| state == MediaPlayerCtrl.MEDIAPLAYER_STATE.END;
	}

	protected override void RunLoad(){
		Diglbug.Log ("Loading MobileTrackVideo " + videoFileName, PrintStream.MEDIA_LOAD);
		mobileControls.Load (videoFileName); // this is where its parameter is set m_str;
	}

	protected override void RunUnload(){
		Diglbug.Log ("Unloading MobileTrackVideo " + videoFileName, PrintStream.MEDIA_LOAD);
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


}
