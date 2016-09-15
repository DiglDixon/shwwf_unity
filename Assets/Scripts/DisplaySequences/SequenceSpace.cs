using UnityEngine;
using System.Collections;

public class SequenceSpace : SequenceElement{

	public float time;

	public override void BeginSequence (){
		StartCoroutine ("RunTime");
	}

	public override void CancelSequence (){
		StopCoroutine("RunTime");
	}

	private IEnumerator RunTime(){
		yield return new WaitForSeconds (time);
		EntranceComplete ();
		ExitComplete ();
	}

}