using UnityEngine;
using UnityEngine.UI;


public class UnexpectedSignalConfirmation : UILightbox{

	private Signal pendingSignal;
	public Text warningText;

	public void OpenWithAttemptedSignal(Signal s){
		pendingSignal = s;
		if (BLE.Instance.Manager.IsExpectingSpecificPayload ()) {
			warningText.text = "Warning! The app is not expecting the payload " + s.GetPayload() + ". Are you sure you want to send it?";
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
