using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class Variables : ConstantSingleton<Variables>{
	public LanguageViewer languageViewer;
	public bool debugBuild = false;

	public Language language;

	public int offsetMinute{ private set; get;}
	public int offsetSecond{ private set; get;}
	public int offsetMillis{ private set; get;}

	protected override void Awake(){
		base.Awake ();
//		Language automaticaLanguage;
		if (Application.systemLanguage == SystemLanguage.Chinese) {
			SetLanguage (Language.MANDARIN);
		} else if (Application.systemLanguage == SystemLanguage.ChineseSimplified) {
			SetLanguage (Language.MANDARIN);
		} else if (Application.systemLanguage == SystemLanguage.ChineseTraditional) {
			SetLanguage (Language.MANDARIN);
		} else if (Application.systemLanguage == SystemLanguage.English) {
			SetLanguage (Language.ENGLISH);
		} else {
			SetLanguage (Language.ENGLISH);
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.O)) {
			Debug.Log("Testing over-hour: "+SignalUtils.GetSignalTimeOffset(new SignalTime(00, 59)));
		}
	}

	public void RestoreTimeOffsetValues(int minute, int second, int millis){
		offsetMinute = minute;
		offsetSecond = second;
		offsetMillis = millis;
	}

	public void SetLanguage(Language l){
		language = l;
		languageViewer.SetLanguage (l);
	}

	public void IncrementAppMillisecond(int millis){
		if (offsetMillis + millis < 0) {
			IncrementAppSecond (-1);
		} else if (offsetMillis + millis >= 1000) {
			IncrementAppSecond (1);
		}

		offsetMillis += millis;
		while (offsetMillis < 0) {
			offsetMillis += 1000;
		}
		offsetMillis = offsetMillis % 1000;
	}

	public void IncrementAppSecond(int seconds){
		if (offsetSecond + seconds < 0) {
			IncrementAppMinute (-1);
		} else if (offsetSecond + seconds >= 60) {
			IncrementAppMinute (1);
		}

		offsetSecond += seconds;
		while (offsetSecond < 0) {
			offsetSecond += 60;
		}
		offsetSecond = offsetSecond % 60;
	}

	public void IncrementAppMinute(int minutes){

		offsetMinute += minutes;
		while (offsetMinute < 0) {
			offsetMinute += 60;
		}
		offsetMinute = offsetMinute % 60;
	}

	// this will need to be called after SetAppSecond, or else it will be overwritten
	public void SetAppMillis(int millis){
		offsetMillis = millis;
	}

	public void SetAppSecond(int second){
		offsetSecond = DateTime.Now.Second - second;
		offsetMillis = DateTime.Now.Millisecond;
	}

	public void SetAppMinute(int minute){
		offsetMinute = DateTime.Now.Minute - minute;
	}

	public DateTime GetCurrentTimeWithOffset(){
		return DateTime.Now.Subtract(new TimeSpan(0, 0, offsetMinute, offsetSecond, offsetMillis));
	}

	public void FinaliseTimeOffsetValues(){
		RecoveryManager.Instance.SetTimeOffsetMinute (offsetMinute);
		RecoveryManager.Instance.SetTimeOffsetSecond (offsetSecond);
		RecoveryManager.Instance.SetTimeOffsetMillis (offsetMillis);
	}
}
