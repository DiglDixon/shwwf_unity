using UnityEngine;

public class QuietStartLoadSetupStep : WaitForPayloadSetupStep{

	public GameObject[] secretButtons;
	public AudioSourceFadeControls toFadeOnComplete;
	public TracklistPlayer player;
	public TracklistEntry entryTrack;

	public override void Activate(ShowSetup callback){
		base.Activate (callback);
		if (ShowMode.Instance.Mode.ModeName == ModeName.GUIDE) {
			for (int k = 0; k < secretButtons.Length; k++) {
				secretButtons[k].SetActive (true);
			}
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