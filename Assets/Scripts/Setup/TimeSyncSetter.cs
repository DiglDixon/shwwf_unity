using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TimeSyncSetter : MonoBehaviour{

	private bool hasHadRoughSet = false;

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

	public void SetRoughSyncFromSignal(Signal s){
		if (!hasHadRoughSet) {
			if (ShowMode.Instance.Mode.ModeName != ModeName.GUIDE) {
				SetModifications(s.GetSignalTime ().second, s.GetSignalTime ().minute);
			}
			hasHadRoughSet = true;
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
	}

	public void Close(){
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