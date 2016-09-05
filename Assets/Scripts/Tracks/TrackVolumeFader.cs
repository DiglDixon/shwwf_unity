using UnityEngine;

public class TrackVolumeFader : LerpFloat{

	private AudioSource sourceToModify;

	private bool lerpPaused = false;

	public TrackVolumeFader(AudioSource sourceToModify, GameObject hostParent){
		this.sourceToModify = sourceToModify;

		GameObject host = GameObject.Instantiate(Resources.Load ("LerpableHost")) as GameObject;
		host.transform.SetParent (hostParent.transform);

		SubscribeProcess (host.GetComponent<LerpableHost>());
	}

	public TrackVolumeFader (AudioSource sourceToModify, LerpableHost host){
		this.sourceToModify = sourceToModify;
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
		sourceToModify.volume = value;
	}

	protected override bool ShouldProcess(){
		if (lerpPaused)
			return false;
		return base.ShouldProcess ();
	}

}

