using UnityEngine;
using System.Collections;

public class TimeSyncSetupStep : SetupStep{

	private bool timeIsSynced = false;
	public TimeSyncSetter timeSyncSetter;
	public UILightbox changeTimeLightbox;

//	private int signalsReceived = 0;
//	private int smallestSignalDifference = int.MaxValue;
//	private int tinySignalThreshold = 2;
//	private int signalWithTinyDifferenceCount = 0;
//
//	private float signalCalibrationTimeout = 7f;
//
//	public GameObject[] enableWhileCalibrating;
//	public GameObject[] disableWhileCalibrating;
//
//	private float alternatingSignalsRate = 5f;
//
//	private bool calibrating = false;
//
//	public GameObject[] fullTinySignalMarks;
//
	protected override bool SetupCompleteCondition (){
		return timeIsSynced;
	}

//	private void SetObjectsActive(GameObject[] objects, bool active){
//		for(int k = 0; k<objects.Length; k++){
//			objects [k].SetActive (active);
//		}
//	}

	public override void Activate (ShowSetup callback){
		base.Activate (callback);
//		BeginAutomaticCalibration ();
	}

	protected override void ResetConditions (){
		timeIsSynced = false;
//		signalsReceived = 0;
//		smallestSignalDifference = int.MaxValue;
//		signalWithTinyDifferenceCount = 0;
//		for (int k = 0; k < fullTinySignalMarks.Length; k++) {
//			fullTinySignalMarks [k].SetActive (false);
//		}
	}

	public void SyncConfirmed(){
		timeIsSynced = true;
		Variables.Instance.FinaliseTimeOffsetValues ();
	}

//	public void CancelCalibration(){
//		EndAutomaticCalibration ();
//	}
//
//	public void BeginAutomaticCalibration(){
//		calibrating = true;
//		SetObjectsActive (enableWhileCalibrating, true);
//		SetObjectsActive (disableWhileCalibrating, false);
//		StartCoroutine ("RunAlternatingSignals");
//	}
//
//	public void EndAutomaticCalibration(){
//		calibrating = false;
//		SetObjectsActive (enableWhileCalibrating, false);
//		SetObjectsActive (disableWhileCalibrating, true);
//		StopCoroutine ("RunAlternatingSignals");
//	}
//
//	private IEnumerator RunAlternatingSignals(){
//		if (ShowMode.Instance.Mode.ModeName == ModeName.GUIDE) {
//			while (true) {
//				BLE.Instance.Manager.ForceSendPayload (Payload.TIME_SYNC_A);
//				yield return new WaitForSeconds (alternatingSignalsRate);
//				BLE.Instance.Manager.ForceSendPayload (Payload.TIME_SYNC_B);
//				yield return new WaitForSeconds (alternatingSignalsRate);
//			}
//		} else {
//			float timePassed = 0f;
//			while (timePassed < signalCalibrationTimeout) {
//				timePassed += Time.deltaTime;
//				yield return null;
//			}
//			CalibrationTimedOut ();
//		}
//	}
//
//	private void CalibrationTimedOut(){
//		EndAutomaticCalibration ();
//		Diglbug.Log ("TimeSync Auto-calibration timed out - resorting to manual calibration", PrintStream.SIGNALS);
//		changeTimeLightbox.Open ();
//	}
//
//	public void AlternatingSignalReceived(Signal s){
//		if (ShowMode.Instance.Mode.ModeName != ModeName.AUDIENCE) {
//			return;
//		}
//		if (!calibrating) {
//			return;
//		}
//			
//		int signalDifference = SignalUtils.GetSignalTimeOffset (s.GetSignalTime());
//		signalDifference = Mathf.Abs (signalDifference);
//
//		if (signalDifference < smallestSignalDifference) {
//			Variables.Instance.SetAppSecond (s.GetSignalTime ().second);
//			Variables.Instance.SetAppMinute (s.GetSignalTime ().minute);
//		}
//		if (signalDifference <= tinySignalThreshold) {
//			TinySignalReceived ();
//		}
//
//		signalsReceived++;
//
//	}
//
//	private void TinySignalReceived(){
//		if (signalWithTinyDifferenceCount >= fullTinySignalMarks.Length) {
//			Diglbug.Log ("ERROR: Unexpected amount of TinySignals received - " + signalWithTinyDifferenceCount, PrintStream.SIGNALS);
//		} else {
//			fullTinySignalMarks [signalWithTinyDifferenceCount].SetActive (true);
//		}
//		signalWithTinyDifferenceCount++;
//		if (signalWithTinyDifferenceCount >= fullTinySignalMarks.Length) {
//			CalibrationSuccessful ();
//		}
//	}
//
//	private void CalibrationSuccessful(){
//		EndAutomaticCalibration ();
//		SyncConfirmed ();
//	}

}