
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DisplaySequence : MonoBehaviour{
	
	private List<SequenceElement> sequences = new List<SequenceElement>();
	private int sequenceIndex;

	protected virtual void Awake(){
		if (SequencesEmpty())
			FindSequences ();
	}

	public virtual void SkipSequence(){
		for (int k = 0; k < sequences.Count; k++) {
			sequences [k].CancelSequence ();
		}
		gameObject.SetActive (false);
	}

	private bool SequencesEmpty(){
		return sequences.Count == 0;
	}

	private void FindSequences(){
		sequences.Clear ();
		SequenceElement newElement;
		for (int k = 0; k < transform.childCount; k++) {
			newElement = transform.GetChild(k).GetComponent<SequenceElement>();
			if (newElement != null) {
				if(!newElement.avoidCollection){
					sequences.Add (newElement);
				}
			}
		}
	}

	protected virtual void OnEnable(){
		if (SequencesEmpty())
			FindSequences ();
		for (int k = 0; k < sequences.Count; k++) {
			sequences [k].SequenceExitedEvent += SequenceExited;
		}
	}

	protected virtual void OnDisable(){
		if (SequencesEmpty())
			FindSequences ();
		for (int k = 0; k < sequences.Count; k++) {
			sequences [k].SequenceExitedEvent -= SequenceExited;
		}
	}

	public virtual void Begin(){
		gameObject.SetActive (true);
		RunSequenceAtIndex (0);
	}

	private void SequenceExited(){
		if (sequenceIndex == sequences.Count - 1) {
			EntireSequenceFinished ();
		} else {
			RunSequenceAtIndex (sequenceIndex+1);
		}
	}

	private void RunSequenceAtIndex(int index){
		sequenceIndex = index;
		sequences [index].BeginSequence ();
	}

	protected virtual void EntireSequenceFinished(){
		//
	}


}
