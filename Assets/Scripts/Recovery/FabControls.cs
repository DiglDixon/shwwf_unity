using UnityEngine;
using UnityEngine.UI;

public class FabControls : SceneControls{

	public Text upcomingText; 
	public UILightbox upcomingGoButton;

	public UILightbox selectSceneLightbox;

	private void Start(){
		if (ShowMode.Instance.IsFabMode ()) {
			if (!RecoveryManager.Instance.RunningRecovery ()) {
				selectSceneLightbox.Open ();
			}
		}
	}

	private void OnEnable(){
		BLE.Instance.Manager.ExpectedPayloadClearedEvent += ExpectedPayloadCleared;
		BLE.Instance.Manager.ExpectedPayloadReadyEvent += ExpectedPayloadBeings;
	}

	private void OnDisable(){
		if (BLE.Instance != null) {
			BLE.Instance.Manager.ExpectedPayloadClearedEvent -= ExpectedPayloadCleared;
			BLE.Instance.Manager.ExpectedPayloadReadyEvent -= ExpectedPayloadBeings;
		}
	}

	private void ExpectedPayloadCleared(){
		upcomingGoButton.Close ();
	}

	private void ExpectedPayloadBeings(Payload p){
		upcomingText.text = ((int)p).ToString ();
		upcomingGoButton.Open ();
	}

	public override void BeginExpectedScene ()
	{
		BLE.Instance.FabModeSignal (BLE.Instance.Manager.GetExpectedSignal());
	}

	public override void BeginCustomAct (Act a)
	{
		Payload p = actSet.GetPayloadForDefinedAct (a.definedAct);
		Signal s = BLE.Instance.Manager.GetSendingSignalWithPayload (p);
		BLE.Instance.FabModeSignal(s);

		selectSceneLightbox.Close ();
	}

}