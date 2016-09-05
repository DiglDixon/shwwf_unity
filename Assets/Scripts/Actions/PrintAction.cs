using UnityEngine;

public class PrintAction : PayloadEventAction{

	public override void FireEvent (Signal s){
		Diglbug.Log ("Testing out the PrintAction "+s.GetPrint());
		Diglbug.LogMobile ("PRINTACTION "+s.GetPrint(), "TEST_PRINT");
	}

}