using UnityEngine;


public class SendPayloadButton : MonoBehaviour{
	public Payload toSend;
	public bool force = false;

	public void ButtonPressed(){
		if (force) {
			BLE.Instance.Manager.ForceSendPayload (toSend);
		} else {
			BLE.Instance.Manager.RequestSendPayload (toSend);
		}
	}
}
