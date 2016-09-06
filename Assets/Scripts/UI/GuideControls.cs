using UnityEngine;
using System;
using UnityEngine.UI;

public class GuideControls : MonoBehaviour{

	public Text upcomingCueText;
	public Text cueStatusText;
	public Button sendExpectedButton;

	private Payload previousPayload;

	private void OnEnable(){
		BLE.Instance.Manager.ExpectedPayloadClearedEvent += ExpectedPayloadCleared;
		BLE.Instance.Manager.ExpectedPayloadReadyEvent += ExpectedPayloadBeings;
		BLE.Instance.Manager.NewSignatureEvent += SignatureUpdated;
		BLE.Instance.Manager.NewUpcomingPayloadEvent += UpcomingPayloadChanged;
	}

	private void OnDisable(){
		BLE.Instance.Manager.ExpectedPayloadClearedEvent -= ExpectedPayloadCleared;
		BLE.Instance.Manager.ExpectedPayloadReadyEvent -= ExpectedPayloadBeings;
		BLE.Instance.Manager.NewSignatureEvent -= SignatureUpdated;
		BLE.Instance.Manager.NewUpcomingPayloadEvent -= UpcomingPayloadChanged;
	}

	private void UpcomingPayloadChanged(Payload p){
		if (p == Payload.NONE) {
			upcomingCueText.text = "(no more cues)";
		} else {
			upcomingCueText.text = p.ToString ();
		}
	}

	private void ExpectedPayloadBeings(Payload p){
		cueStatusText.text = "Cue ready to send.";
		sendExpectedButton.interactable = true;
	}

	private void ExpectedPayloadCleared(){
		cueStatusText.text = "Not expecting any cues.";
		sendExpectedButton.interactable = false;
	}

	private void SignatureUpdated(Signature s){
		// nothing yet.
	}

//	private void Update(){
//		if (BLE.Instance.Manager.IsExpectingSpecificPayload ()) {
//			previousPayload = BLE.Instance.Manager.GetExpectedPayload ();
//			upcomingCueText.text = previousPayload.ToString ();
//
//			sendExpectedButton.interactable = true;
//		} else {
//			string[] names = Enum.GetNames(typeof(Payload));
//			if(((int)previousPayload) < names.Length-1){
//				upcomingCueText.text = names[((int)previousPayload)+1];
//				cueStatusText.text = "Not ready to send cue.";
//			}else{
//				upcomingCueText.text = "";
//				cueStatusText.text = "No more cues remaining";
//			}
////			upcomingCueText.text = "(not expecting any cues yet";
//			sendExpectedButton.interactable = false;
//		}
//	}

}