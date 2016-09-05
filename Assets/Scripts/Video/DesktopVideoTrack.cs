using UnityEngine;

public class DesktopVideoTrack : VideoTrack{

	public Texture2D placeholderImage;
	public string placeholderName = "placeholderName";

	public AudioClip placeholderClip;

	private AudioTrack audioTrack;

	private float inverse_trackLength;
	private float trackLength;

	private void Awake(){
		audioTrack = gameObject.AddComponent<AudioTrack> ();
		audioTrack.SetAudioClip (placeholderClip);

		trackLength = placeholderClip.length;
		inverse_trackLength = 1f / trackLength;
	}

	public AudioTrack GetAudioTrack(){
		return audioTrack;
	}

	public Texture2D GetPlaceholderImage(){
		return placeholderImage;
	}

	public override string GetTrackName(){
		return "d_"+GetComponent<MobileVideoTrack>().GetTrackName();
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

	protected override bool ShouldLoad (){
		return true;
	}

	protected override bool ShouldUnload (){
		return true;
	}

	protected override void RunLoad(){
		Diglbug.Log ("Loading track (Placeholder) " + GetTrackName(), PrintStream.VIDEO);
	}


	protected override void RunUnload(){
		Diglbug.Log ("Unloading track( Placeholder) " + GetTrackName(), PrintStream.VIDEO);
	}

	public override bool IsLoaded(){
		return true;
	}

	public override bool IsLoading(){
		return false;
	}

	private void OnValidate()
	{
		gameObject.name = GetTrackName ();
	}


}