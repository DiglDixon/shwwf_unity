using UnityEngine;
using System;
using UnityEngine.UI;

public class GuideControls : MonoBehaviour{

	public DefinedActText upcomingActText;
	public Text cueStatusText;
	public Button sendExpectedButton;

	public ActSet actSet;
	private ShowAct act;

	public Slider actProgressSlider;
	public Slider markerSlider;

	public Text[] timeUntilExpectedTexts;
	public Text[] englishTimeUntilExpectedTexts;
	public Text[] mandarinTimeUntilExpectedTexts;

	public Text nextSceneReadyText;

	private Payload previousPayload;

	public GameObject largeControls;

	public TracklistPlayer player;

	private void OnEnable(){
		BLE.Instance.Manager.ExpectedPayloadClearedEvent += ExpectedPayloadCleared;
		BLE.Instance.Manager.ExpectedPayloadReadyEvent += ExpectedPayloadBeings;
//		BLE.Instance.Manager.NewSignatureEvent += SignatureUpdated;
		BLE.Instance.Manager.NewUpcomingPayloadEvent += UpcomingPayloadChanged;

		actSet.ActChangedEvent += ActChanged;
	}

	private void OnDisable(){
		if (BLE.Instance != null) {
			BLE.Instance.Manager.ExpectedPayloadClearedEvent -= ExpectedPayloadCleared;
			BLE.Instance.Manager.ExpectedPayloadReadyEvent -= ExpectedPayloadBeings;
			//		BLE.Instance.Manager.NewSignatureEvent -= SignatureUpdated;
			BLE.Instance.Manager.NewUpcomingPayloadEvent -= UpcomingPayloadChanged;
		}
		actSet.ActChangedEvent -= ActChanged;
	}

	public void BeginExpectedScene(){
		player.SendExpectedActWhenLoaded ();
	}

	public void BeginCustomScene(Payload p){
		// BLE.Instance.Manager.ForceSendPayload(p); // the old way
	}


	public void OpenLargeControls(){
		largeControls.SetActive (true);
	}

	public void CloseLargeControls(){
		largeControls.SetActive (false);
	}

	private void ActChanged(Act a){
		act = (ShowAct)a;
		markerSlider.value = act.GetLastTrackStartProgress ();
		// modify the positions.
	}

	private void Update(){
		if (act != null) {
			actProgressSlider.value = act.GetProgress();
			for (int k = 0; k < timeUntilExpectedTexts.Length; k++) {
				timeUntilExpectedTexts[k].text = Utils.AudioTimeFormat(act.GetTimeUntilExpected ());
			}
		}
	}

	private void UpcomingPayloadChanged(Payload p){
		if (p == Payload.NONE) {
			upcomingActText.gameObject.SetActive (false);
		} else {
			upcomingActText.gameObject.SetActive (true);
			upcomingActText.UpdateValue(actSet.GetDefinedActForPayload(p));
		}
	}

	private void ExpectedPayloadBeings(Payload p){
		cueStatusText.text = "Ready to begin next scene.";
		nextSceneReadyText.gameObject.SetActive (true);
		for (int k = 0; k < englishTimeUntilExpectedTexts.Length; k++) {
			englishTimeUntilExpectedTexts [k].gameObject.SetActive (false);
		}
		for (int k = 0; k < mandarinTimeUntilExpectedTexts.Length; k++) {
			mandarinTimeUntilExpectedTexts [k].gameObject.SetActive (false);
		}
		OpenLargeControls ();
		sendExpectedButton.interactable = true;
	}

	private void ExpectedPayloadCleared(){
		if (Variables.Instance.language == Language.ENGLISH) {
			for (int k = 0; k < englishTimeUntilExpectedTexts.Length; k++) {
				englishTimeUntilExpectedTexts [k].gameObject.SetActive (true);
			}
		} else {
			for (int k = 0; k < mandarinTimeUntilExpectedTexts.Length; k++) {
				mandarinTimeUntilExpectedTexts [k].gameObject.SetActive (true);
			}
		}
		nextSceneReadyText.gameObject.SetActive (false);
		cueStatusText.text = "";
		sendExpectedButton.interactable = false;
	}

}