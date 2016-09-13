using UnityEngine;
using System.Collections;

public class SequenceObjectGroup : SequenceElement{

	public SequenceObject[] objects;

	public float stagger = 0.25f;
	public float lingerAfterEntranceDuration = 2f;
	private WaitForSeconds staggerYield;

	private int enteredObjects = 0;
	private int exitedObjects = 0;

	private void Awake(){
		staggerYield = new WaitForSeconds (stagger);
	}

	private void OnEnable(){
		for (int k = 0; k < objects.Length; k++) {
			objects [k].SequenceEnteredEvent += ObjectEntranceComplete;
			objects [k].SequenceExitedEvent += ObjectExitComplete;
		}
	}

	private void OnDisable(){
		for (int k = 0; k < objects.Length; k++) {
			objects [k].SequenceExitedEvent -= ObjectEntranceComplete;
			objects [k].SequenceExitedEvent -= ObjectExitComplete;
		}
	}

	public override void CancelSequence(){
		for (int k = 0; k < objects.Length; k++) {
			objects [k].CancelSequence ();
		}
		StopCoroutine ("RunSequence");
	}

	public override void BeginSequence (){
		StartCoroutine ("RunSequence");
	}

	private void ObjectEntranceComplete(){
		enteredObjects++;
		if (enteredObjects == objects.Length) {
			EntranceComplete ();
		}
	}

	private void ObjectExitComplete(){
		exitedObjects++;
		if (exitedObjects == objects.Length) {
			ExitComplete ();
		}
	}

	private IEnumerator RunSequence(){
		for (int k = 0; k < objects.Length; k++) {
			objects [k].BeginSequence ();
			yield return staggerYield;
		}
	}

	protected override void EntranceComplete (){
		base.EntranceComplete ();
		StartCoroutine (RunLinger ());
	}

	private IEnumerator RunLinger(){
		yield return new WaitForSeconds (lingerAfterEntranceDuration);
		for (int k = 0; k < objects.Length; k++) {
			objects [k].EndSequence ();
		}
	}

}