using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowSetup : MonoBehaviour {

	private SetupStep currentStep;
	private SetupStep[] steps;
	public Text stepInstructionText;

	public SetupStepDisplay[] displays;

	private int stepIndex = 0;

	private void Awake(){
		steps = GetComponentsInChildren<SetupStep> ();
	}

	private void Start(){
		for (int k = 0; k < steps.Length; k++) {
			if (k >= displays.Length) {
				Diglbug.LogWarning ("Not enough SetupStepDisplays found to accommodate all steps " + k); 
			} else {
				steps [k].SetDisplay (displays [k]);
			}
			steps [k].Deactivate ();
		}
		ActivateStepAtIndex (0);
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
		stepInstructionText.text = currentStep.description;
	}

	private void StepsComplete(){
		Diglbug.Log ("All Setup steps complete.", PrintStream.SETUP);
		SceneManager.LoadScene (Scenes.Show);
	}

}
