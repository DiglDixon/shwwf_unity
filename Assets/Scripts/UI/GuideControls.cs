using UnityEngine;
using System;
using UnityEngine.UI;

public class GuideControls : MonoBehaviour{

	public Text upcomingCueText;
	public Text cueStatusText;
	public Button sendExpectedButton;

	private Payload previousPayload;

	private void Update(){
		if (BLE.Instance.Manager.IsExpectingSpecificPayload ()) {
			previousPayload = BLE.Instance.Manager.GetExpectedPayload ();
			upcomingCueText.text = previousPayload.ToString ();
			cueStatusText.text = "Cue ready to send.";
			sendExpectedButton.interactable = true;
		} else {
			string[] names = Enum.GetNames(typeof(Payload));
			if(((int)previousPayload) < names.Length-1){
				upcomingCueText.text = names[((int)previousPayload)+1];
				cueStatusText.text = "Not ready to send cue.";
			}else{
				upcomingCueText.text = "";
				cueStatusText.text = "No more cues remaining";
			}
//			upcomingCueText.text = "(not expecting any cues yet";
			sendExpectedButton.interactable = false;
		}
	}

}