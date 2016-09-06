
using UnityEngine;

public class PayloadSelectionDisplay : ListDisplay{

	public PayloadEventSystem payloadSystem;

	void Awake(){
		PayloadEvent[] payloads = payloadSystem.GetComponentsInChildren<PayloadEvent>();
		for (int k = 0; k < payloads.Length; k++) {
			AddListItem (payloads [k]);
		}
	}

	public override void ItemPressed (int itemIndex){
		base.ItemPressed (itemIndex);
		Diglbug.Log ("Custom PayloadSelected: " + (Payload)itemIndex, PrintStream.SIGNALS);
		BLE.Instance.Manager.RequestPayloadSend ((Payload)itemIndex);
		Close ();
	}
}