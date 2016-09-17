using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeSyncSetup : MonoBehaviour{

	public Text secondsText;
	public Text minutesText;
	public Text hoursText;

	private TimeSpan mod = new TimeSpan();

	public Text timeDisplay;

	public void AutomaticallySetOffsetToSignal(Signal s){

	}

	public void NewTimeSubmitted(){

	}

	public void Update(){
		if (Input.GetKeyDown (KeyCode.H)) {
			mod = new TimeSpan (mod.Days, 1, mod.Minutes, mod.Seconds, mod.Milliseconds);
		}
		if (Input.GetKeyDown (KeyCode.H)) {
			mod = new TimeSpan (mod.Days, 1, mod.Minutes, mod.Seconds, mod.Milliseconds);
		}
		DateTime cTime = DateTime.Now.Subtract (mod);
		timeDisplay.text = ("TIME_MOD: "+cTime.Hour+":"+cTime.Minute+":"+cTime.Second);
	}

}