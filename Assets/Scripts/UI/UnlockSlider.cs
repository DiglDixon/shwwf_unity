using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Slider))]
public class UnlockSlider : MonoBehaviour {

	public LockScreen lockScreen;
	private Slider slider;
	private float minSliderValueToUnlock = 0.9f;

	private void Awake(){
		slider = GetComponent<Slider> ();
	}

	public void Pressed(){
		// nothing?
	}

	public void Released(){
		if (slider.value >= minSliderValueToUnlock) {
			lockScreen.Close ();
		}
		slider.value = 0f;
	}

}
