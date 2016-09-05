
using UnityEngine;

public class MobileVideoVolumeFader : LerpFloat{

	private MediaPlayerCtrl controlsToModify;

	private bool lerpPaused = false;

	public MobileVideoVolumeFader(MediaPlayerCtrl controlsToModify, GameObject hostParent){
		this.controlsToModify = controlsToModify;

		GameObject host = GameObject.Instantiate(Resources.Load ("LerpableHost")) as GameObject;
		host.transform.SetParent (hostParent.transform);

		SubscribeProcess (host.GetComponent<LerpableHost>());
	}

	public MobileVideoVolumeFader (MediaPlayerCtrl controlsToModify, LerpableHost host){
		this.controlsToModify = controlsToModify;
		SubscribeProcess (host);
	}

	public void CancelFades(){
		base.CancelLerp ();
		lerpPaused = false;
	}

	public void PauseFades(){
		lerpPaused = true;
	}

	public void UnpauseFades(){
		lerpPaused = false;
	}

	public void FadeVolumeTo(float value, float time){
		base.LerpTo (value, time);
	}

	protected override void LerpStepValue(float value){
		controlsToModify.SetVolume(value);;
	}

	protected override bool ShouldProcess(){
		if (lerpPaused)
			return false;
		return base.ShouldProcess ();
	}

}

