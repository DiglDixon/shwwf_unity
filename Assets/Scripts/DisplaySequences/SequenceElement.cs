using UnityEngine;

public abstract class SequenceElement : MonoBehaviour{

	public delegate void SequenceEnteredDelegate();
	public SequenceEnteredDelegate SequenceEnteredEvent;

	public delegate void SequenceExitedDelegate();
	public SequenceExitedDelegate SequenceExitedEvent;

	public abstract void BeginSequence();

	protected virtual void EntranceComplete(){
		if (SequenceEnteredEvent != null) {
			SequenceEnteredEvent ();
		}
	}

	protected virtual void ExitComplete(){
		if (SequenceExitedEvent != null) {
			SequenceExitedEvent ();
		}
	}

}