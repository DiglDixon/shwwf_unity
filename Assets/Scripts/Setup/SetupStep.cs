using UnityEngine;
using System.Collections;

public abstract class SetupStep : MonoBehaviour {

	private ShowSetup callback;
	public SetupStepDisplay display;

	public bool updateAudienceInstructions = true;
	public bool updateGuideInstructions = true;

	[TextArea]
	public string descriptionEnglish;
	[TextArea]
	public string descriptionMandarin;

	[TextArea]
	public string descriptionEnglishGuide;
	[TextArea]
	public string descriptionMandarinGuide;

	void Awake(){
		
	}

	public virtual void Activate(ShowSetup callback){
//		if (ShowMode.Instance.Mode.ModeName == ModeName.GUIDE) {
//			if (updateGuideInstructions) {
//				description = Variables.Instance.language == Language.ENGLISH ? descriptionEnglishGuide : descriptionMandarinGuide;
//			}
//		} else {
//			if (updateAudienceInstructions) {
//				description = Variables.Instance.language == Language.ENGLISH ? descriptionEnglish : descriptionMandarin;
//			}
//		}
		gameObject.SetActive (true);
		ResetConditions ();
		if (display != null) {	
			display.SetIncomplete ();
		}
		this.callback = callback;
	}

	public virtual void Deactivate(){
		gameObject.SetActive (false);
	}

	protected virtual void Update(){
		if (SetupCompleteCondition ()) {
			ConditionCompleted ();
			callback.SetupStepComplete ();
			if (display != null) {
				display.SetComplete ();
			}
			Deactivate ();
		}
	}

	protected virtual void ConditionCompleted(){
		//
	}

	public virtual void SkipStep(){
		//
	}

	// This may have been unnecessary - if we're using scene loads these should reset themselves.
	protected abstract void ResetConditions ();

	public void SetDisplay(SetupStepDisplay display){
		this.display = display;
		display.SetIdle ();
	}

	protected abstract bool SetupCompleteCondition ();

}
