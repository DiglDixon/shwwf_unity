using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LerpFloat : Lerpable {

	private float startValue;
	private float destinationValue;

	public delegate void LerpEndsValueCallback(float endValue);
	public event LerpEndsValueCallback LerpEndsValueEvents;

	public delegate void LerpStepValueDelegate(float value);
	public event LerpStepValueDelegate LerpStepValueEvent;

	private float currentValue;

	public void SetLerpFloatValue(float value){
		currentValue = value;
		LerpStepValue (currentValue);
	}

	protected override void LerpStep(float step){
		currentValue = Mathf.Lerp(startValue, destinationValue, step);
		LerpStepValue (currentValue);
	}

	protected virtual void LerpStepValue(float value){
		if (LerpStepValueEvent != null) {
			LerpStepValueEvent (value);
		}
	}

	public void AddLerpEndsCallback(LerpEndsValueCallback callback){
		LerpEndsValueEvents += callback;
	}

	protected override void LerpEnds (){
		base.LerpEnds ();
		if (LerpEndsValueEvents != null)
			LerpEndsValueEvents (currentValue);
	}

	public virtual void LerpTo(float destination, float time){
		startValue = currentValue;
		destinationValue = destination;
		base.BeginLerpOverTime (time);
	}

}
