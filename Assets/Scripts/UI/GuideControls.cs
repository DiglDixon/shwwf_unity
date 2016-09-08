using UnityEngine;
using System;
using UnityEngine.UI;

public class GuideControls : MonoBehaviour{

	public Text upcomingCueText;
	public Text cueStatusText;
	public Button sendExpectedButton;

	public ActSet actSet;
	private Act act;

	public Slider actProgressSlider;
	public Slider markerSlider;

	private Payload previousPayload;

	private void OnEnable(){
		BLE.Instance.Manager.ExpectedPayloadClearedEvent += ExpectedPayloadCleared;
		BLE.Instance.Manager.ExpectedPayloadReadyEvent += ExpectedPayloadBeings;
		BLE.Instance.Manager.NewSignatureEvent += SignatureUpdated;
		BLE.Instance.Manager.NewUpcomingPayloadEvent += UpcomingPayloadChanged;

		actSet.ActChangedEvent += ActChanged;
	}

	private void OnDisable(){
		BLE.Instance.Manager.ExpectedPayloadClearedEvent -= ExpectedPayloadCleared;
		BLE.Instance.Manager.ExpectedPayloadReadyEvent -= ExpectedPayloadBeings;
		BLE.Instance.Manager.NewSignatureEvent -= SignatureUpdated;
		BLE.Instance.Manager.NewUpcomingPayloadEvent -= UpcomingPayloadChanged;

		actSet.ActChangedEvent -= ActChanged;
	}

	private void ActChanged(Act a){
		act = a;
		markerSlider.value = act.GetLastTrackStartProgress ();
		// modify the positions.
	}

	private void Update(){
		if (act != null) {
			actProgressSlider.value = act.GetProgress();
		}
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

}