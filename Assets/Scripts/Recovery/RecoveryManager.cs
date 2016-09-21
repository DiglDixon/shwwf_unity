using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class RecoveryManager : ConstantSingleton<RecoveryManager> {

	private const string lastSignalMinuteKey = "sig_min";
	private const string lastSignalSecondKey = "sig_sec";
	private const string lastSignalPayloadKey = "sig_pay";
	private const string lastSignalSignatureKey = "sig_sig";

	private const string oldSignatureKey = "old_sig";
	private const string oldModeKey = "old_mode";

	private const string showUnderwayKey = "underway";

	private const string lastActorKey = "actor";

	private const string offsetMinuteKey = "off_min";
	private const string offsetSecondKey = "off_sec";
	private const string offsetMillisKey = "off_millis";

	private const string fabModeKey = "fab";

	public UILightbox recoveryResumeLightbox;
	public UILightbox actorecoveryResumeLightbox;
	public Text previousActorText;

	private bool runningRecovery = false;

	private bool showUnderway = false;

	public GameObject recoverLoadScreen;
	public GameObject recoverFromPauseScreen;

	protected override void Awake(){
		base.Awake ();
		bool wasUnderway = (PlayerPrefs.GetInt (showUnderwayKey, 0) == 1);
		if (wasUnderway) {
			runningRecovery = true;
		}
	}

	protected void Start(){
		
		bool wasUnderway = (PlayerPrefs.GetInt (showUnderwayKey, 0) == 1);
		if (wasUnderway) {
			ModeName prevMode = (ModeName)PlayerPrefs.GetInt (oldModeKey);
			if (prevMode == ModeName.ACTOR) {
				actorecoveryResumeLightbox.Open ();
				Actor previousActor = (Actor)PlayerPrefs.GetInt (lastActorKey, -1);

				if (Variables.Instance.language == Language.ENGLISH) {
					previousActorText.text = EnumDisplayNamesEnglish.ActorName (previousActor);
				} else {
					previousActorText.text = EnumDisplayNamesMandarin.ActorName (previousActor);
				}
			} else {
				recoveryResumeLightbox.Open ();
			}
		}
	}

	public void ResumeDismissed(){
		runningRecovery = false;
		ShowNotUnderway ();
	}

	public void ResumeAccepted(){
		Diglbug.Log ("Beginning Recovery resume..", PrintStream.RECOVERY);
		StartCoroutine (RunRecovery ());
	}

	private IEnumerator RunRecovery(){
		runningRecovery = true;
		recoverLoadScreen.SetActive (true);

		RecoverMode ();

		yield return null;
		if (ShowMode.Instance.Mode.ModeName == ModeName.ACTOR) {
			SceneManager.LoadScene (Scenes.Actor);
		} else {
			RecoverSignature ();
			SceneManager.LoadScene (Scenes.MonoScene);
		}
		yield return new WaitForSeconds (0.5f);
		while (!SceneManager.GetActiveScene ().isLoaded) {
			yield return null;
		}

		int oldOffsetMinute = PlayerPrefs.GetInt (offsetMinuteKey, -1);
		int oldOffsetSecond = PlayerPrefs.GetInt (offsetSecondKey, -1);
		int oldOffsetMillis = PlayerPrefs.GetInt (offsetMillisKey, -1);
		Diglbug.Log ("Restoring time offset: " + oldOffsetMinute + ", " + oldOffsetSecond + ", " + oldOffsetMillis, PrintStream.RECOVERY);
		Variables.Instance.RestoreTimeOffsetValues (oldOffsetMinute, oldOffsetSecond, oldOffsetMillis);

		Recover ();
	}

	private void RecoverMode(){

		int oldModeName = PlayerPrefs.GetInt (oldModeKey, -1);
		if (oldModeName == -1) {
			Diglbug.Log ("Old Mode Name was -1 - this should never happen!", PrintStream.RECOVERY);
		}
		// This needs to come after we set the mode
		bool wasFabMode = (PlayerPrefs.GetInt (fabModeKey, 0) == 1);

		ShowMode.Instance.SetMode ((ModeName)oldModeName);

		ShowMode.Instance.SetFabMode (wasFabMode);
	}

	private void RecoverSignature(){
		int oldSignature = PlayerPrefs.GetInt(oldSignatureKey, 0);
		if (oldSignature == 0) {
			Diglbug.Log ("Old Signature was None! This may happen for Actors", PrintStream.RECOVERY);
		}
		ShowMode.Instance.Signature = (Signature)oldSignature;
	}

	private void Recover(){
		Diglbug.Log ("Beginning Recovery...", PrintStream.RECOVERY);
//		RecoverSignature ();

		runningRecovery = false;
		ShowUnderway ();

		if (ShowMode.Instance.Mode.ModeName == ModeName.GUIDE) {
			BLE.Instance.ClearPreviousSignalsFound ();
			BLE.Instance.Manager.RecoverySendPreviousSignal (GetPreviousSignal());
			Diglbug.Log ("Guide recovery complete");

		} else if (ShowMode.Instance.Mode.ModeName == ModeName.ACTOR){
			BLE.Instance.ClearPreviousSignalsFound ();
			ActorPlayer player = FindObjectOfType<ActorPlayer> ();
			if (player != null) {
				player.ClearPreviousSignals ();
				player.SetActor ((Actor)PlayerPrefs.GetInt (lastActorKey, 0));
			} else {
				Diglbug.Log ("Unexpected lack of ActorPlayer object during Actor Recovery.");
			}
			RecoverFromMostRecentSignal ();
			Diglbug.Log ("Actor recovery complete, waiting for signals");

		} else {
			RecoverFromMostRecentSignal ();
			Diglbug.Log ("Recovery complete - waiting to pick up signals");
		}
	}

	public void RecoverFromMostRecentSignal(){
		RecoverSignature ();
		Signal previous = GetPreviousSignal ();
		if (previous != null) {
			if (ShowMode.Instance.IsFabMode ()) {
				BLE.Instance.FabModeSignal (previous);
			} else {
				BLE.Instance.Manager.RecoveryReceivePreviousSignal (previous);
			}
		}
		BLE.Instance.ClearPreviousSignalsFound ();
	}

	private Signal GetPreviousSignal(){
		Signature previousSignature = (Signature)PlayerPrefs.GetInt (lastSignalSignatureKey, 0);
		Payload previousPayload = (Payload)PlayerPrefs.GetInt (lastSignalPayloadKey, 0);
		int previousMinute = PlayerPrefs.GetInt (lastSignalMinuteKey, -1);
		int previousSecond = PlayerPrefs.GetInt (lastSignalSecondKey, -1);
		if (previousMinute == -1 || previousSecond == 1) {
			return null;
		} else {
			return new Signal (previousSignature, previousPayload, previousMinute, previousSecond);
		}
	}

	public void RecoveryComplete(){
		recoverLoadScreen.SetActive (false);
		runningRecovery = false;
	}

	public void ResumeComplete(){
		Diglbug.Log ("Recovery resume complete!", PrintStream.RECOVERY);
		runningRecovery = false;
	}

	public bool RunningRecovery(){
		return runningRecovery;
	}

	public void ChosenAsActor(Actor a){
		PlayerPrefs.SetInt (lastActorKey, (int)a);
	}

	public void ShowUnderway(){
		if (runningRecovery) {
			Diglbug.Log ("Blocked a ShowUnderway call while we're running recovery", PrintStream.RECOVERY);
			return;
		}
		showUnderway = true;
		PlayerPrefs.SetInt (showUnderwayKey, 1);
	}

	public void ShowNotUnderway(){
		if (runningRecovery) {
			Diglbug.Log ("Blocked a ShowUnderway call while we're running recovery", PrintStream.RECOVERY);
			return;
		}
		showUnderway = false;
		PlayerPrefs.SetInt (showUnderwayKey, 0);
	}

	public bool ResumeAvailable(){
		return showUnderway;
	}

	public void ResumeRequested(){
		ResumeAccepted ();
	}

	public void SetFabMode(bool v){
		PlayerPrefs.SetInt (fabModeKey, v ? 1 : 0);
	}

	public void SignalReceived(Signal s){
		if (runningRecovery) {
			Diglbug.Log ("Blocked a signal while we're running recovery", PrintStream.RECOVERY);
			return;
		}
		if (s.GetPayload () == Payload.EMERGENCY_PAUSE || s.GetPayload () == Payload.EMERGENCY_UNPAUSE) {
			Diglbug.Log ("Blocked saving emergency pause signal", PrintStream.RECOVERY);
			return;
		}
		Diglbug.Log ("Saving accepted signal: " + s.GetFullPrint (), PrintStream.RECOVERY);
		PlayerPrefs.SetInt (lastSignalMinuteKey, s.GetSignalTime ().minute);
		PlayerPrefs.SetInt (lastSignalSecondKey, s.GetSignalTime ().second);
		PlayerPrefs.SetInt (lastSignalPayloadKey, (int)s.GetPayload());
		PlayerPrefs.SetInt (lastSignalSignatureKey, (int)s.GetSignature());
	}

	public void SignatureSet(Signature s){
		PlayerPrefs.SetInt (oldSignatureKey, (int)s);
	}

	public void SetMode(ModeName modeName){
		PlayerPrefs.SetInt(oldModeKey, (int)modeName);
	}

	public void SetTimeOffsetMinute(int minuteOff){
		PlayerPrefs.SetInt (offsetMinuteKey, minuteOff);
		Diglbug.Log("Setting Minute offset: "+PlayerPrefs.GetInt(offsetMinuteKey, -1), PrintStream.RECOVERY);
	}
	public void SetTimeOffsetSecond(int secondOff){
		PlayerPrefs.SetInt (offsetSecondKey, secondOff);
		Diglbug.Log("Setting Second offset: "+PlayerPrefs.GetInt(offsetSecondKey, -1), PrintStream.RECOVERY);
	}
	public void SetTimeOffsetMillis(int millisOff){
		PlayerPrefs.SetInt (offsetMillisKey, millisOff);
		Diglbug.Log("Setting Millis offset: "+PlayerPrefs.GetInt(offsetMillisKey, -1), PrintStream.RECOVERY);
	}

//	private void OnApplicationFocus( bool focusStatus )
//	{
//		Diglbug.Log("Application focused: "+focusStatus, PrintStream.RECOVERY);
//	}

	private void OnApplicationPause( bool pauseStatus )
	{
		Diglbug.Log("Application paused: "+pauseStatus, PrintStream.RECOVERY);
		if (pauseStatus == false) {
			if (showUnderway) {
				if (!RunningRecovery ()) {
					Recover ();
				}
			}
		}
	}


}
