using UnityEngine;
using System.Collections;

public class PayloadEvent : EnsurePayloadChild {

	public Payload payload;
	private PayloadEventAction[] actions;

	private void Awake(){
		actions = GetComponentsInChildren<PayloadEventAction> ();
	}

	public override void SetPayload(Payload p){
		payload = p;
		transform.SetSiblingIndex ((int)payload);
		UpdateName ();
	}

	public override Payload GetPayload(){
		return payload;
	}

	public void AddComponentToObject(GameObject go){
		go.AddComponent(this.GetType());

	}

	protected override string GetNameString(){
		int eventCount = GetComponentsInChildren<PayloadEventAction> ().Length;
		return "("+eventCount+") "+payload.ToString ()+" event";
	}

	public void FireEvents(Signal s){
		for (int k = 0; k < actions.Length; k++) {
			actions [k].FireEvent (s);
		}
	}

//	public override GameObject ConstructListObject (){
//		GameObject ret = GameObject.Instantiate(Resources.Load("Payload_Entry_List_Item")) as GameObject;
//		PayloadEntryListDisplayItem display = ret.GetComponent<PayloadEntryListDisplayItem> ();
//		display.SetPayload (payload);
//		return ret;
//	}

}
