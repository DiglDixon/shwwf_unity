
public class SetExpectingPayloadEvent : CustomTrackTimeEvent{

	public Payload expectedPayload;

	public override void CustomEvent (){
		BLE.Instance.Manager.PayloadExpected (expectedPayload);
	}

	#if UNITY_EDITOR
	protected override string GetObjectName(){
		return this.GetType ().Name + " - "+expectedPayload;
	}
	#endif

}