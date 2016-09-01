using UnityEngine;

public class DesktopVideoTrack : VideoTrack{

	[SerializeField]
	private MovieTexture videoTexture;

	private AudioTrack audioTrack;

	private float inverse_trackLength;
	private float trackLength;

	private void Awake(){
		trackLength = videoTexture.duration;
		inverse_trackLength = 1f / trackLength;
		audioTrack = gameObject.AddComponent<AudioTrack> ();
		audioTrack.SetAudioClip (videoTexture.audioClip);
	}

	public MovieTexture GetVideoTexture(){
		return videoTexture;
	}

	public AudioTrack GetAudioTrack(){
		return audioTrack;
	}

	public override string GetTrackName(){
		return "d_video_"+videoTexture.name;
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

	public override void Load(){
		Diglbug.Log ("Loading track " + GetTrackName(), PrintStream.VIDEO);
		// Let's null this out, it's only for testing anyway.
//		desktopTexture.Load (mobileControls.m_strFileName);
	}


	public override void Unload(){
		Diglbug.Log ("Unloading track " + GetTrackName(), PrintStream.VIDEO);
		//
	}

	public override bool IsLoaded(){
		return true;//
	}

	public override float FadeTime(){
		return 0;
	}

	private void OnValidate()
	{
		gameObject.name = GetTrackName ();
	}


}
