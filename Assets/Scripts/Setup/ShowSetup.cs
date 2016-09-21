using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowSetup : MonoBehaviour {

	private SetupStep currentStep;
	private SetupStep[] steps;
	public Text stepInstructionText;
	public TracklistPlayer player;

	public AudioSourceFadeControls ensureFadedOutOnStart;

	public DisplaySequence introSequence;

	public GameObject setupDisplay;

	public SignatureSelector signatureSelector;

	private int stepIndex = 0;

	private void Awake(){
		steps = GetComponentsInChildren<SetupStep> ();
	}

	private void Start(){
		Diglbug.Log ("Setup Start Called");
		for (int k = 0; k < steps.Length; k++) {
			steps [k].Deactivate ();
		}
		ActivateStepAtIndex (0);
		BLE.Instance.SetupBegins ();
		if (RecoveryManager.Instance.RunningRecovery () || ShowMode.Instance.IsFabMode ()) {
			SkipSetup ();
			setupDisplay.SetActive (false);
		} else if (ShowMode.Instance.SkippingSetup ()) {
			setupDisplay.SetActive (false);
			SkipSetup ();
			signatureSelector.ForceChange ();
		}else {
			Diglbug.Log ("Enabling setup display");
			setupDisplay.SetActive (true);
		}
	}


	private void Update(){
		if (Input.GetKeyDown (KeyCode.K)) {
			SkipSetup ();
		}
	}

	public void SkipSetup(){
		for (int k = 0; k < steps.Length; k++) {
			steps [k].SkipStep ();
		}
		signatureSelector.ResetSignatureDisplays ();
		StepsComplete (true);
	}

	public void SetupStepComplete(){
		ActivateNextStep ();
	}

	private void ActivateNextStep(){
		ActivateStepAtIndex (stepIndex + 1);
	}

	private void ActivateStepAtIndex(int index){
		if (index >= steps.Length) {
			StepsComplete (false);
		}
		else {
			if (currentStep) {
				currentStep.Deactivate ();
			}
			stepIndex = index;
			ActivateStep (steps [index]);
		}
	}

	public void BeginPressed(){
		BLE.Instance.Manager.ForceSendPayload (Payload.BEGIN_SHOW);
	}

	public void ActivateStep(SetupStep step){
		Diglbug.Log ("Activating SetupStep " + step.name);
		currentStep = step;
		currentStep.Activate (this);
		if (ShowMode.Instance.Mode.ModeName == ModeName.GUIDE) {
			if (currentStep.updateGuideInstructions) {
				if (Variables.Instance.language == Language.ENGLISH) {
					stepInstructionText.text = currentStep.descriptionEnglishGuide;
				} else {
					stepInstructionText.text = currentStep.descriptionMandarinGuide;
				}
			}
		} else {
			if (currentStep.updateAudienceInstructions) {
				if (Variables.Instance.language == Language.ENGLISH) {
					stepInstructionText.text = currentStep.descriptionEnglish;
				} else {
					stepInstructionText.text = currentStep.descriptionMandarin;
				}
			}
		}
	}

	private void StepsComplete(bool skipped){
		Diglbug.Log ("All Setup steps complete. Beginning show!", PrintStream.SETUP);
		ensureFadedOutOnStart.FadeTo (0f);
		if (skipped) {
			if (ShowMode.Instance.IsFabMode ()) {
					BLE.Instance.Manager.PayloadExpected (Payload.BEGIN_SHOW);
					if (ShowMode.Instance.Mode.ModeName == ModeName.GUIDE) {
						player.SendExpectedActWhenLoaded ();
					}
			}
		} else {
			introSequence.Begin ();
		}
		if (RecoveryManager.Instance.RunningRecovery ()) {
			RecoveryManager.Instance.RecoveryComplete ();
		}
		RecoveryManager.Instance.ShowUnderway ();
		BLE.Instance.EnableJockeyProtection ();
		BLE.Instance.SetupEnds ();
	}

}
