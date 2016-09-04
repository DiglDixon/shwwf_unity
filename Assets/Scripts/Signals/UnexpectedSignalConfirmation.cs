using UnityEngine;
using UnityEngine.UI;


public class UnexpectedSignalConfirmation : UILightbox{

	private Signal pendingSignal;
	public Text warningTextExpected;
	public Text warningTextAttempted;

	public void OpenWithAttemptedSignal(Signal s){
		pendingSignal = s;
		if (BLE.Instance.Manager.IsExpectingSpecificPayload ()) {
			warningTextExpected.text = BLE.Instance.Manager.GetExpectedPayload ().ToString ();
			warningTextAttempted.text = s.GetPayload ().ToString ();
		}
		// TODO: (else if it's not expecting anything...)
		Open();
	}

	public void AcceptPressed(){
		BLE.Instance.Manager.ForceSignalSend(pendingSignal);
	}

	public void CancelPressed(){
		Close();
	}

}
