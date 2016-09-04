using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Slider))]
public class TrackSlider : MonoBehaviour{

	public TrackUIControls display;
//	public TrackOutput trackOutput;
	private Slider slider;
	private bool heldDown = false;

	private void Awake(){
		slider = GetComponent<Slider> ();
	}

	public void UpdateDisplayValue(float v){
		if (!heldDown) {
			slider.value = v;
		}
	}

	public float GetValue(){
		return slider.value;
	}

	public void SliderPressed(){
		heldDown = true;
		display.ScrubPositionBegins ();
	}

	public void SliderReleased(){
		heldDown = false;
		display.ScrubPositionEnds (slider.value);
	}
}
