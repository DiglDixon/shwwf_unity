using UnityEngine;
using System.Collections;


public class IntroSequence : MonoBehaviour{

	public GameObject[] showWhenStarted;
	public SequenceObject background;
	private SequenceElement[] sequences;
	private int sequenceIndex;

	public GameObject[] hideOnceEntered;

	public void Update(){
		if (Input.GetKeyDown (KeyCode.I)) {
			Begin ();
		}
	}

	private void Awake(){
		if (sequences == null)
			FindSequences ();
	}

	private void FindSequences(){
		sequences = GetComponentsInChildren<SequenceElement> ();
		Diglbug.Log ("Found " + sequences.Length + " sequences");
	}

	private void OnEnable(){
		if (sequences == null)
			FindSequences ();
		for (int k = 0; k < sequences.Length; k++) {
			sequences [k].SequenceExitedEvent += SequenceExited;
		}
		background.SequenceEnteredEvent += BackgroundEntered;
	}

	private void OnDisable(){
		if (sequences == null)
			FindSequences ();
		for (int k = 0; k < sequences.Length; k++) {
			sequences [k].SequenceExitedEvent -= SequenceExited;
		}
		background.SequenceEnteredEvent -= BackgroundEntered;
	}

	public void Begin(){
		for (int k = 0; k < showWhenStarted.Length; k++) {
			showWhenStarted [k].SetActive (true);
		}
		sequenceIndex = 0;
		RunSequenceAtIndex (0);
	}

	private void BackgroundEntered(){
		sequenceIndex = 1;
		RunSequenceAtIndex (1);
		for (int k = 0; k < hideOnceEntered.Length; k++) {
			hideOnceEntered [k].SetActive (false);
		}
	}

	private void SequenceExited(){
		if (sequenceIndex == sequences.Length - 1) {
			EntireSequenceFinished ();
		} else {
			sequenceIndex++;
			RunSequenceAtIndex (sequenceIndex);
		}
	}

	private void RunSequenceAtIndex(int index){
		sequences [index].BeginSequence ();
	}

	private void EntireSequenceFinished(){
		background.EndSequence();
	}


}
