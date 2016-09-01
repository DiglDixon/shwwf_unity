using UnityEngine;
using System.Collections;

public abstract class Lerpable {

	protected bool lerpRunning { get; private set;}

	private float lerpStep;
	private float currentTime;
	private float destinationTime;
	private float inverse_destinationTime;

	public delegate void LerpEndsCallback();
	public event LerpEndsCallback LerpEndsEvents;

	public virtual void SubscribeProcess(LerpableHost host){
		host.UpdateLerpEvent += ProcessFrame;
	}

	public void ProcessFrame(){
		if (ShouldProcess()) {
			if (currentTime < destinationTime) {
				lerpStep = ApplyLerpEquation ();
				LerpStep (lerpStep);
				currentTime += Time.deltaTime;
			} else {
				LerpStep (1f);
				lerpRunning = false;
				LerpEnds ();
			}
		}
	}

	protected virtual bool ShouldProcess(){
		return lerpRunning;
	}

	// The alternative way of doing this - desktop tests showed massive overheads with this method.
//	private void Update(){
//		ProcessFrame ();
//	}

	// TODO: Extend these into interesting easings
	protected virtual float ApplyLerpEquation(){
		return Mathf.Lerp(0f, 1f, currentTime * inverse_destinationTime);
	}

	protected virtual void BeginLerpOverTime(float time){
		if (lerpRunning)
			OverrideLerp ();
		LerpBegins ();
		StartLerp (time);
	}

	private void StartLerp(float time){
		currentTime = 0f;
		destinationTime = time;
		inverse_destinationTime = 1f / destinationTime + 0.0001f;  // this is to avoid divide-by-zeroes.
		lerpStep = 0f;
		lerpRunning = true;
	}

	private void StopLerp(){
		lerpRunning = false;
	}

	protected void OverrideLerp(){
		StopLerp();
	}

	protected void CancelLerp(){
		StopLerp();
		LerpCancelled ();
	}

	protected virtual void LerpEnds (){
		if (LerpEndsEvents != null)
			LerpEndsEvents ();
	}

	protected virtual void LerpBegins (){
	}
	protected abstract void LerpStep (float step);
	protected virtual void LerpCancelled(){
	}

}
