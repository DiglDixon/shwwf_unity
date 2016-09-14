using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowSetup : MonoBehaviour {

	private SetupStep currentStep;
	private SetupStep[] steps;
	public Text stepInstructionText;
	public TracklistPlayer player;

	public AudioSourceFadeControls ensureFadedOutOnStart;

	public IntroSequence introSequence;

	public GameObject setupDisplay;

	private int stepIndex = 0;

	private void Awake(){
		steps = GetComponentsInChildren<SetupStep> ();
		setupDisplay.SetActive (true);
	}

	private void Start(){
		for (int k = 0; k < steps.Length; k++) {
			steps [k].Deactivate ();
		}
		ActivateStepAtIndex (0);
	}


	private void Update(){
		if (Input.GetKeyDown (KeyCode.K)) {
			for (int k = 0; k < steps.Length; k++) {
				steps [k].SkipStep ();
			}
			StepsComplete ();
		}
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
		introSequence.Begin ();
		ensureFadedOutOnStart.FadeTo (0f);
		BLE.Instance.EnableJockeyProtection ();
		player.Play ();
	}

}
