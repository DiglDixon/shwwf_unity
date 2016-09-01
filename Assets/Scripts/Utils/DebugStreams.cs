using UnityEngine;
using System.Collections;


public class DebugStreams : MonoBehaviour{

	public PrintStream[] closedStreams;

	void Start(){
		for (int k = 0; k < closedStreams.Length; k++) {
			Diglbug.DisablePrintStream (closedStreams [k]);
		}
//		StartCoroutine (PrintSequence ());
	}

//	private IEnumerator PrintSequence(){
//		Diglbug.LogMobile ("one", "COUNT");
//		yield return new WaitForSeconds (2f);
//		Diglbug.LogMobile ("two", "COUNT");
//		yield return new WaitForSeconds (2f);
//		Diglbug.LogMobile ("new", "NEW");
//		yield return new WaitForSeconds (2f);
//		Diglbug.LogMobile ("three", "COUNT");
//	}

}