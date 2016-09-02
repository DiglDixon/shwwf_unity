using UnityEngine;
using System.Collections;

public abstract class SetupStep : MonoBehaviour {

	private ShowSetup callback;
	private SetupStepDisplay display;

	[TextArea]
	public string description;

	public virtual void Activate(ShowSetup callback){
		gameObject.SetActive (true);
		ResetConditions ();
		this.callback = callback;
	}

	public virtual void Deactivate(){
		gameObject.SetActive (false);
	}

	protected virtual void Update(){
		if (SetupCompleteCondition ()) {
			callback.SetupStepComplete ();
			SetDisplayValue (true);
			Deactivate ();
		}
	}

	// This may have been unnecessary - if we're using scene loads these should reset themselves.
	protected abstract void ResetConditions ();

	public void SetDisplay(SetupStepDisplay display){
		this.display = display;
		SetDisplayValue (false); // this should be elsewhere.
	}

	protected void SetDisplayValue(bool completed){
		if (display) {
			if (completed) {
				display.SetComplete ();
			} else {
				display.SetIncomplete ();
			}
		}
	}

	protected abstract bool SetupCompleteCondition ();

}
