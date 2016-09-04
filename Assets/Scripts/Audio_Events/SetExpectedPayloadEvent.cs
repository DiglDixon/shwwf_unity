
public class SetExpectedPayloadEvent : CustomTrackTimeEvent{

	public Payload expectedPayload;

	public override void CustomEvent (){
		BLE.Instance.Manager.SetExpectedPayload (expectedPayload);
	}

	#if UNITY_EDITOR
	protected override string GetObjectName(){
		return this.GetType ().Name + " - "+expectedPayload;
	}
	#endif

}