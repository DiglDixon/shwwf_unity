using UnityEngine;
using System;
using UnityEngine.UI;

public class GuideControls : SceneControls{

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

	public GuidePhotos guidePhotos;
	public Image guideImage;

	private void OnEnable(){
		BLE.Instance.Manager.ExpectedPayloadClearedEvent += ExpectedPayloadCleared;
		BLE.Instance.Manager.ExpectedPayloadReadyEvent += ExpectedPayloadBeings;
//		BLE.Instance.Manager.NewSignatureEvent += SignatureUpdated;
		BLE.Instance.Manager.NewUpcomingPayloadEvent += UpcomingPayloadChanged;

		actSet.ActChangedEvent += ActChanged;
		actSet.FinalActBeginsEvent += FinalActReached;
	}

	private void OnDisable(){
		if (BLE.Instance != null) {
			BLE.Instance.Manager.ExpectedPayloadClearedEvent -= ExpectedPayloadCleared;
			BLE.Instance.Manager.ExpectedPayloadReadyEvent -= ExpectedPayloadBeings;
			//		BLE.Instance.Manager.NewSignatureEvent -= SignatureUpdated;
			BLE.Instance.Manager.NewUpcomingPayloadEvent -= UpcomingPayloadChanged;
		}
		actSet.ActChangedEvent -= ActChanged;
		actSet.FinalActBeginsEvent -= FinalActReached;
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

	private void FinalActReached(Act a){
		Diglbug.Log ("Guide display Final Act reached: " + a.definedAct);
		upcomingActText.gameObject.SetActive (false);
		guideImage.overrideSprite = guidePhotos.GetFinalActSprite ();
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
		Diglbug.Log ("Upcoming guide display payload changed: " + p);
		// This is a hack around some nasty OOO stuff during setup.
		if (p == Payload.BEGIN_SHOW) {
			p = Payload.HAMBURGER;
		}
		if (p == Payload.NONE) {
//			upcomingActText.gameObject.SetActive (false);
		} else {
			upcomingActText.gameObject.SetActive (true);
			DefinedAct upcomingAct = actSet.GetDefinedActForPayload (p);
			if (Variables.Instance.language == Language.ENGLISH) {
				upcomingActText.UpdateValue(upcomingAct);
			} else {
				upcomingActText.UpdateValue(upcomingAct);
			}

			guideImage.overrideSprite = guidePhotos.GetSpriteForAct (upcomingAct);
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
		Diglbug.Log ("Guide Display opening for Expected Payload " + p);
		if ((int)p > (int)Payload.BEGIN_SHOW) {
			OpenLargeControls ();
		}
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