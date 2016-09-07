using UnityEngine;
using System.Collections;

public class SendBluetoothToggle : MonoBehaviour {
	public Payload sendOnTrue;
	public Payload sendOnFalse;

	public void Send(bool sendOne){
		if (sendOne) {
			BLE.Instance.Manager.ForceSendPayload (sendOnTrue);
		} else {
			BLE.Instance.Manager.ForceSendPayload (sendOnFalse);
		}
	}
}
