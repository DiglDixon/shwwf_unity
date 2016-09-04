using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Toggle))]
public class DebugStreamOption : MonoBehaviour {
	public PrintStream stream;
	public DebugStreams callback;
	private Toggle myToggle;

	void Awake(){
		myToggle = GetComponent<Toggle> ();
	}

	public void ForceValue(bool v){
		if (!myToggle) {
			myToggle = GetComponent<Toggle> ();
		}
		myToggle.isOn = v;
	}

	public void ValueChanged(bool value){
		callback.OptionPressed (this, value);
	}

}
