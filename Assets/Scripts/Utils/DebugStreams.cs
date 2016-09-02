using UnityEngine;
using System.Collections;


public class DebugStreams : MonoBehaviour{

	public PrintStream[] closedStreams;

	void Start(){
		for (int k = 0; k < closedStreams.Length; k++) {
			Diglbug.DisablePrintStream (closedStreams [k]);
		}
	}

}