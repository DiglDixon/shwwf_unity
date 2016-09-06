
public class ClearExpectedPayloadEvent : CustomTrackTimeEvent{

	public Payload expectedPayload;

	public override void CustomEvent (){
		BLE.Instance.Manager.ClearExpectedPayload ();
	}

	#if UNITY_EDITOR
	protected override string GetObjectName(){
		return "CLEAR_PAYLOAD - " + this.GetType ().Name;
	}
	#endif

}