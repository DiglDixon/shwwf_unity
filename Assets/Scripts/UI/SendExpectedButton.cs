using UnityEngine;
using UnityEngine.UI;

public class SendExpectedButton : MonoBehaviour{

	public void ButtonPressed(){
		BLE.Instance.Manager.SendExpectedPayload ();
	}

}
