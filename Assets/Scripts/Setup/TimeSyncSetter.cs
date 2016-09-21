using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TimeSyncSetter : MonoBehaviour{

	public Text minuteText;
	public Text secondText;
	public FontSizeLerp minuteFontSizeLerp;
	public FontSizeLerp secondFontSizeLerp;

	private int lastConfirmedMinute;
	private int lastConfirmedSecond;

	private float bounceFontSize = 140f;
	private float restFontSize = 100f;
	private float bounceReturnTime = 0.25f;

	private int previousMinute;
	private int previousSecond;

	private int minuteAtOpen;
	private int secondAtOpen;
	private int millisAtOpen;

	public UILightbox lightbox;

	private bool sendingSignalA = true;
	private float alternatingSignalRate = 4f;

	private bool firstRoughSet = false;

	private bool runningAutomaticSet = false;
	private float automaticSetTimeout = 10f;
	public GameObject[] automaticSetLoadingObjects;

	public void SetRoughSyncFromSignal(Signal s){
		if (!firstRoughSet) {
			RefreshRoughSignal (s);
			firstRoughSet = true;
		} else if (runningAutomaticSet) {
			RefreshRoughSignal (s);
			StopAutomaticSet ();
		}
	}

	public void StartAutomaticSet(){
		if (!runningAutomaticSet) {
			runningAutomaticSet = true;
			for (int k = 0; k < automaticSetLoadingObjects.Length; k++) {
				automaticSetLoadingObjects [k].SetActive (true);
			}
//			automaticSetLoadingObject.SetActive (true);
			StartCoroutine ("RunAutomaticSet");
		}
	}

	private IEnumerator RunAutomaticSet(){
		float elapsed = 0f;
		while (elapsed < automaticSetTimeout) {
			elapsed += Time.deltaTime;
			yield return null;
		}
		// timed out
		StopAutomaticSet();
		Diglbug.Log ("Run Automatic Time Sync Set timed out", PrintStream.SIGNALS);
	}

	private void StopAutomaticSet(){
		if (runningAutomaticSet) {
			runningAutomaticSet = false;
			for (int k = 0; k < automaticSetLoadingObjects.Length; k++) {
				automaticSetLoadingObjects [k].SetActive (false);
			}
			StopCoroutine ("RunAutomaticSet");
		}
	}

	private void Update(){
		DateTime offsetTime = Variables.Instance.GetCurrentTimeWithOffset ();
		if (offsetTime.Minute != previousMinute) {
			minuteFontSizeLerp.lerper.SetLerpFloatValue (bounceFontSize);
			minuteFontSizeLerp.lerper.LerpTo (restFontSize, bounceReturnTime);
		}
		if (offsetTime.Second != previousSecond) {
			secondFontSizeLerp.lerper.SetLerpFloatValue (bounceFontSize);
			secondFontSizeLerp.lerper.LerpTo (restFontSize, bounceReturnTime);
		}
		minuteText.text = offsetTime.Minute.ToString("00");
		secondText.text = offsetTime.Second.ToString ("00");
		previousMinute = offsetTime.Minute;
		previousSecond = offsetTime.Second;
	}

	public void MillisUpPressed(){
		Variables.Instance.IncrementAppMillisecond (-200);
	}

	public void MillisDownPressed(){
		Variables.Instance.IncrementAppMillisecond (300);
	}

	public void Open(){
		lightbox.Open ();
		minuteAtOpen = Variables.Instance.offsetMinute;
		secondAtOpen = Variables.Instance.offsetSecond;
		millisAtOpen = Variables.Instance.offsetMillis;
		if (ShowMode.Instance.Mode.ModeName == ModeName.GUIDE) {
			StartCoroutine ("RunAlternatingSignals");
		}
	}

	public void RefreshRoughSignal(Signal s){
		if (ShowMode.Instance.Mode.ModeName != ModeName.GUIDE) {
			SetModifications(s.GetSignalTime().minute, s.GetSignalTime().second);
		}
	}

	private IEnumerator RunAlternatingSignals(){
		while (true) {
			if (sendingSignalA) {
				BLE.Instance.Manager.ForceSendPayload (Payload.TIME_SYNC_A);
			} else {
				BLE.Instance.Manager.ForceSendPayload (Payload.TIME_SYNC_B);
			}
			sendingSignalA = !sendingSignalA;
			yield return new WaitForSeconds (alternatingSignalRate);
		}
	}

	public void AcceptAndClose(){
		Close ();
	}

	private void Close(){
		if (ShowMode.Instance.Mode.ModeName == ModeName.GUIDE) {
			StopCoroutine ("RunAlternatingSignals");
		}
		lightbox.Close ();
	}

	private void SetModifications(int minute, int second){
		Variables.Instance.SetAppMinute (minute);
		Variables.Instance.SetAppSecond (second);
	}

	public void CancelModifications(){
		Variables.Instance.RestoreTimeOffsetValues (minuteAtOpen, secondAtOpen, millisAtOpen);
		Close ();
	}

}



/* PRE-SIMPLIFIED
 * 
public class TimeSyncSetter : MonoBehaviour{

	private bool hasHadRoughSet = false;

	public TimeAdjustSlider minuteAdjustSlider;
	public TimeAdjustSlider secondAdjustSlider;

	public void SetRoughSyncFromSignal(Signal s){
		if (!hasHadRoughSet) {
			if (ShowMode.Instance.Mode.ModeName != ModeName.GUIDE) {
				Variables.Instance.SetAppSecond (s.GetSignalTime ().second);
				Variables.Instance.SetAppMinute (s.GetSignalTime ().minute);
			}
			hasHadRoughSet = true;
		}
	}

	private void OnEnable(){
		minuteAdjustSlider.ValueChangedEvent += MinuteSliderChanged;
		secondAdjustSlider.ValueChangedEvent += SecondSliderChanged;
	}

	private void OnDisable(){
		minuteAdjustSlider.ValueChangedEvent -= MinuteSliderChanged;
		secondAdjustSlider.ValueChangedEvent -= SecondSliderChanged;
	}

	private void Update(){
		DateTime offsetTime = Variables.Instance.GetCurrentTimeWithOffset ();
		minuteAdjustSlider.UpdateValueFromTime(offsetTime.Minute);
		secondAdjustSlider.UpdateValueFromTime(offsetTime.Second);
	}

	public void MinuteSliderChanged(int v){
		// we update both to stop awkward minute-skips.
		Variables.Instance.SetAppSecond (secondAdjustSlider.GetCurrentValue ());
		Variables.Instance.SetAppMinute (v);
	}

	public void SecondSliderChanged(int v){
		Variables.Instance.SetAppSecond (v);
		Variables.Instance.SetAppMinute (minuteAdjustSlider.GetCurrentValue ());
	}


}*/