using UnityEngine;
using System.Collections;


public class DebugStreams : MonoBehaviour{
	
	public bool useAllButClosed = false;
	public PrintStream[] closedStreams;
	public PrintStream[] openStreams;

	void Start(){
		if (useAllButClosed) {
			Diglbug.SetAllPrintStream (true);
			for (int k = 0; k < closedStreams.Length; k++) {
				Diglbug.DisablePrintStream (closedStreams [k]);
			}
		} else {
			Diglbug.SetAllPrintStream (false);
			for (int k = 0; k < openStreams.Length; k++) {
				Diglbug.EnablePrintStream (openStreams [k]);
			}
		}
	}

}