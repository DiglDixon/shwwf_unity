using UnityEngine;

public class EnableDebugBLECommand : DebugCommand{

	public GameObject DebugBLEPanel;

	public override bool isValidCommand (string c){
		return c=="bleon" || c=="bleoff";
	}

	public override void RunCommand (string c){
		if (c == "bleon") {
			DebugBLEPanel.SetActive (true);
		}
		if (c == "bleoff") {
			DebugBLEPanel.SetActive(false);
		}

	}

}