
public class SetExpectedPayloadEvent : CustomTrackTimeEvent{

	public Payload expectedPayload;

	public override void CustomEvent (){
		BLE.Instance.Manager.SetExpectedPayload (expectedPayload);
	}

}