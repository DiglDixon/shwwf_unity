using UnityEngine;
using System.Collections;

public class StopBLESendingButton : MonoBehaviour {

	public void ButtonPressed(){
		BLE.Instance.Manager.StopSending ();
//		BLE.Instance.Manager.StartReceiving ();
	}
}
