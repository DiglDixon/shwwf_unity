using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class Variables : ConstantSingleton<Variables>{
	public LanguageViewer languageViewer;
	public bool debugBuild = false;

	public Language language;

	private int offsetMinute;
	private int offsetSecond;
	private int offsetMillis;

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

	public void RestoreTimeOffsetValues(int minute, int second, int millis){
		offsetMinute = minute;
		offsetSecond = second;
		offsetMillis = millis;
	}

	public void SetLanguage(Language l){
		language = l;
		languageViewer.SetLanguage (l);
	}

	public void SetAppSecond(int second){
		offsetSecond = DateTime.Now.Second - second;
		offsetMillis = DateTime.Now.Millisecond;
	}

	public void SetAppMinute(int minute){
		offsetMinute = DateTime.Now.Minute - minute;
	}

	// this will need to be called after SetAppSecond, or else it will be overwritten
	public void SetAppMillis(int millis){
		offsetMillis = millis;
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
