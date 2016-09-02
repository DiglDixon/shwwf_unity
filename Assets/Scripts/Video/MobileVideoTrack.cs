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

	public override void Load(){
		Diglbug.Log ("Loading track " + GetTrackName(), PrintStream.VIDEO);
		mobileControls.Load (videoFileName); // this is where its parameter is set m_str;
		// unfortunately, now is the only time we can call these.
		trackLength = mobileControls.GetDuration ();
		inverse_trackLength = 1f / trackLength;
	}


	public override void Unload(){
		Diglbug.Log ("Unloading track " + GetTrackName(), PrintStream.VIDEO);
		mobileControls.UnLoad ();
	}

	public override bool IsLoaded(){
		return mobileControls.GetCurrentState () == MediaPlayerCtrl.MEDIAPLAYER_STATE.READY;
	}

	public override float FadeTime(){
		return 0f;
	}

	private void OnValidate()
	{
		gameObject.name = GetTrackName ();
	}


}
