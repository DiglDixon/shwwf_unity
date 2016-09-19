using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TimeSyncSetter : MonoBehaviour{

	private bool hasHadRoughSet = false;

	public Text minuteText;
	public Text secondText;

	public void SetRoughSyncFromSignal(Signal s){
		if (!hasHadRoughSet) {
			if (ShowMode.Instance.Mode.ModeName != ModeName.GUIDE) {
				Variables.Instance.SetAppSecond (s.GetSignalTime ().second);
				Variables.Instance.SetAppMinute (s.GetSignalTime ().minute);
			}
			hasHadRoughSet = true;
		}
	}

	private void Update(){
		DateTime offsetTime = Variables.Instance.GetCurrentTimeWithOffset ();
		minuteText.text = offsetTime.Minute.ToString("00");
		secondText.text = offsetTime.Second.ToString ("00");
	}

	public void MillisUpPressed(){
		Variables.Instance.IncrementAppMillisecond (-100);
	}

	public void MillisDownPressed(){
		Variables.Instance.IncrementAppMillisecond (200);
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