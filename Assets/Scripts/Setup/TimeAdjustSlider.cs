using UnityEngine;
using UnityEngine.UI;


public class TimeAdjustSlider : MonoBehaviour{

	public float maxValue = 10f;

	public Slider slider;
	public Text valueText;

	public delegate void ValueChangedDelegate(int v);
	public ValueChangedDelegate ValueChangedEvent;

	public delegate void PressedDelegate();
	public PressedDelegate PressedEvent;

	private bool sliderHeldDown = false;

	private int currentValue = 0;

	public void UpdateValueFromTime(int v){
		if (!sliderHeldDown) {
			currentValue = v;
			UpdateValueDisplay (currentValue);
		}
	}

	private void Update(){
		if (sliderHeldDown) {
			int v = currentValue + GetSliderValue ();
			while (v < 0f) {
				v += 60;
			}
			v = v % 60;
			UpdateValueDisplay (v);
		}
	}

	private void UpdateValueDisplay(int v){
		valueText.text = v.ToString("00");
	}

	public void SliderDown(){
		if (ShowMode.Instance.Mode.ModeName == ModeName.GUIDE) {
			return;
		}

		sliderHeldDown = true;
		if (PressedEvent != null) {
			PressedEvent ();
		}
	}

	public void SliderUp(){
		if (ShowMode.Instance.Mode.ModeName == ModeName.GUIDE) {
			return;
		}

		sliderHeldDown = false;
		Diglbug.Log (name+" slider value changing, was: " + currentValue);
		currentValue += GetSliderValue();
		Diglbug.Log (name+" now is: " + currentValue);
		if (ValueChangedEvent != null) {
			ValueChangedEvent (currentValue);
		}
		slider.value = 0.5f;

	}

	private int GetSliderValue(){
		float sliderValue = slider.value;
		// norm, signed
		sliderValue -= 0.5f;
		sliderValue *= 2f;

		// store sign
		float sign = sliderValue > 0f? 1f : -1f;

		// apply exponent (cubed)
		sliderValue = Mathf.Abs (sliderValue);
		sliderValue = sliderValue * sliderValue * sliderValue;

		// restore sign
		sliderValue *= sign;

		// scale
		sliderValue *= maxValue;

		return Mathf.RoundToInt(sliderValue);
	}

	public int GetCurrentValue(){
		if (sliderHeldDown) {
			return currentValue + GetSliderValue ();
		} else {
			return currentValue;
		}
	}


}
