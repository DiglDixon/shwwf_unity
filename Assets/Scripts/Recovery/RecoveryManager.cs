using UnityEngine;
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

	public UILightbox recoveryResumeLightbox;

	private bool runningRecovery = false;

	private bool showUnderway = false;

	public GameObject recoverLoadScreen;

	protected override void Awake(){
		base.Awake ();
		bool wasUnderway = (PlayerPrefs.GetInt (showUnderwayKey, 0) == 1);
		if (wasUnderway) {
			recoveryResumeLightbox.Open ();
			runningRecovery = true;
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
		int oldModeName = PlayerPrefs.GetInt (oldModeKey, -1);
		if (oldModeName == -1) {
			Diglbug.Log ("Old Mode Name was -1 - this should never happen!", PrintStream.RECOVERY);
		}
		ShowMode.Instance.SetMode ((ModeName)oldModeName);
		yield return null;
		SceneManager.LoadScene (Scenes.MonoScene);
		yield return new WaitForSeconds (0.5f);
		while (!SceneManager.GetActiveScene ().isLoaded) {
			yield return null;
		}
		Recover ();
	}

	private void Recover(){
		Diglbug.Log ("Beginning Recovery...", PrintStream.RECOVERY);
		int oldSignature = PlayerPrefs.GetInt(oldSignatureKey, 0);
		if (oldSignature == 0) {
			Diglbug.Log ("Old Signature was None! This should not happen", PrintStream.RECOVERY);
		}
		ShowMode.Instance.Signature = (Signature)oldSignature;

		runningRecovery = false;
		ShowUnderway ();
		if (ShowMode.Instance.Mode.ModeName == ModeName.GUIDE) {
			Signature previousSignature = (Signature)PlayerPrefs.GetInt (lastSignalSignatureKey, 0);
			Payload previousPayload = (Payload)PlayerPrefs.GetInt (lastSignalPayloadKey, 0);
			int previousMinute = PlayerPrefs.GetInt (lastSignalMinuteKey, 0);
			int previousSecond = PlayerPrefs.GetInt (lastSignalSecondKey, 0);
			Signal s = new Signal (previousSignature, previousPayload, previousMinute, previousSecond);
			BLE.Instance.Manager.RecoverFromPreviousSignal (s);
			Diglbug.Log ("Guide recovery complete");
		} else {
			BLE.Instance.ClearPreviousSignalsFound (); // this shouldn't be required, but doesn't hurt.
			Diglbug.Log ("Recovery complete - waiting to pick up signals");
		}
	}

	public void RecoveryComplete(){
		recoverLoadScreen.SetActive (false);
	}

	public void ResumeComplete(){
		Diglbug.Log ("Recovery resume complete!", PrintStream.RECOVERY);
		runningRecovery = false;
	}

	public bool RunningRecovery(){
		return runningRecovery;
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

	public void ShowMinimised(){

	}

	public void SignalSent(Signal s){
		if (runningRecovery) {
			Diglbug.Log ("Blocked a signal while we're running recovery", PrintStream.RECOVERY);
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

	private void OnApplicationFocus( bool focusStatus )
	{
		Diglbug.Log("Application focused: "+focusStatus, PrintStream.RECOVERY);
//		if (focusStatus == true) {
//			if (showUnderway) {
//				Recover ();
//			}
//		}
	}

	private void OnApplicationPause( bool pauseStatus )
	{
		Diglbug.Log("Application paused: "+pauseStatus, PrintStream.RECOVERY);
//		isPaused = pauseStatus;
		if (pauseStatus == true) {
			if (showUnderway) {
				Recover ();
			}
		}
	}


}
