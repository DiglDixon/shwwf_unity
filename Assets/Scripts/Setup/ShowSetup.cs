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
		for (int k = 0; k < steps.Length; k++) {
			steps [k].Deactivate ();
		}
		ActivateStepAtIndex (0);
		BLE.Instance.SetupBegins ();
		if (RecoveryManager.Instance.RunningRecovery ()) {
			SkipSetup ();
			setupDisplay.SetActive (false);
		} else {
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
		StepsComplete ();
	}

	public void SetupStepComplete(){
		ActivateNextStep ();
	}

	private void ActivateNextStep(){
		ActivateStepAtIndex (stepIndex + 1);
	}

	private void ActivateStepAtIndex(int index){
		if (index >= steps.Length) {
			StepsComplete ();
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

	private void StepsComplete(){
		Diglbug.Log ("All Setup steps complete. Beginning show!", PrintStream.SETUP);
		ensureFadedOutOnStart.FadeTo (0f);
		if (!RecoveryManager.Instance.RunningRecovery()) {
			introSequence.Begin ();
			BLE.Instance.Manager.PayloadExpected (Payload.BEGIN_SHOW);
			if (ShowMode.Instance.Mode.ModeName == ModeName.GUIDE) {
				player.SendExpectedActWhenLoaded ();
			}
		}
		RecoveryManager.Instance.ShowUnderway ();
		BLE.Instance.EnableJockeyProtection ();
		BLE.Instance.SetupEnds ();
	}

}
