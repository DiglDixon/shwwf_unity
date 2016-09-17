using UnityEngine;

public class TimeSyncSetupStep : ReceiveSignalSetupStep{

	private bool timeIsSynced = false;
	public TimeSyncSetup timeSyncSetup;
	public UILightbox changeTimeLightbox;

	private float differenceDetectThreshold = 3f;
	private bool runningCheck = false;
	private float confirmDetectThreshold = 5f;

	public override void Activate (ShowSetup callback){
		base.Activate (callback);
//		float timeDifference = SignalUtils.GetSignalTimeOffset(
	}

	public override void ReceiveSignal(Signal s){
		float difference = SignalUtils.GetSignalTimeOffset (s.GetSignalTime());
		if (runningCheck) {
			if (difference > confirmDetectThreshold) {
				Diglbug.Log ("Custom set proved ineffective. Asking to manually check and sync.", PrintStream.DEBUGGING);
				RunCheckProcess (s);
			} else {
				CheckWasSuccessful ();
			}
		} else {
			if (difference > differenceDetectThreshold) {
				Diglbug.Log ("Likely time sync issue. Asking to manually check and sync.", PrintStream.DEBUGGING);
				RunCheckProcess (s);
			} else {
				CheckWasSuccessful ();
			}
		}
	}

	private void RunCheckProcess(Signal s){
		runningCheck = true;
		timeSyncSetup.AutomaticallySetOffsetToSignal (s);
		changeTimeLightbox.Open ();
	}

	private void CheckWasSuccessful(){
		timeIsSynced = true;
	}

	protected override void ResetConditions (){
		runningCheck = false;
		timeIsSynced = false;
	} 

	protected override bool SetupCompleteCondition (){
		return true;
//		return timeIsSynced;
	}

}