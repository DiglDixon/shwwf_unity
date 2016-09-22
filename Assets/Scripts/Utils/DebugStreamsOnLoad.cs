using UnityEngine;


public class DebugStreamsOnLoad : MonoBehaviour{

	public bool disableAll = false;
	public PrintStream[] exceptions;

	private void Awake(){
		Diglbug.Log("DebugStreamsOnLoad Running", PrintStream.PRINT_STREAMS);
		Diglbug.SetAllPrintStream(!disableAll);
		for (int k = 0; k < exceptions.Length; k++) {
			if(disableAll){
				Diglbug.EnablePrintStream (exceptions [k]);
			}else{
				Diglbug.DisablePrintStream (exceptions [k]);
			}
		}
	}


}
