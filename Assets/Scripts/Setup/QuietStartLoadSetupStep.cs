using UnityEngine;

public class QuietStartLoadSetupStep : WaitForPayloadSetupStep{

	public GameObject secretButton;
	public AudioSourceFadeControls toFadeOnComplete;
	public TracklistPlayer player;
	public TracklistEntry entryTrack;

	public override void Activate(ShowSetup callback){
		base.Activate (callback);
		if (ShowMode.Instance.Mode.ModeName == ModeName.GUIDE) {
			secretButton.SetActive (true);
		}
		player.PrepareTrack (entryTrack);
	}

	public override void SignalReceived (){
		base.SignalReceived ();
		toFadeOnComplete.FadeTo (0f);
	}

	public override void SkipStep (){
		base.SkipStep ();
		player.PrepareTrack (entryTrack);
	}

}