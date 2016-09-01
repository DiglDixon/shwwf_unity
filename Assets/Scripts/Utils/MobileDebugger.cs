using UnityEngine;
using UnityEngine.UI;

public class MobileDebugger : MonoBehaviour{
	public Text buttonCountText;
	private int logCount = 0;
	private int channelCount = 0;

	public GameObject textsParent;

	private Text[] debugTexts;
	private int debugIndex = 0;

	private void Start(){
		if (debugTexts == null) {
			InitialiseDebugTexts ();
		}
	}

	// TODO: This is really nasty. Please fix it when less tired.
	public void LogMessage(string message, string channel){
		if (debugTexts == null) {
			InitialiseDebugTexts ();
		}
		bool encountered = false;
		for (int k = 0; k < debugTexts.Length; k++) {
			if (debugTexts [k].text.Contains (channel)) {
				debugTexts [k].text = channel + ": " + message;
				encountered = true;
				break;
			}
		}
		if (!encountered) {
			channelCount++;
			debugIndex = (debugIndex + 1) % debugTexts.Length;
			debugTexts [debugIndex].text = channel + ": " + message;
		}
		logCount = (logCount+1) % 100;
		UpdateButtonCountText ();
	}

	private void UpdateButtonCountText(){
		buttonCountText.text = "("+logCount+"), ("+channelCount+")";
	}

	private void InitialiseDebugTexts(){
		debugTexts = textsParent.GetComponentsInChildren<Text> ();
	}


}
