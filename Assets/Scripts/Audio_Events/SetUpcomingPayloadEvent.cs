
public class SetUpcomingPayloadEvent : CustomTrackTimeEvent{

	public Payload upcomingPayload;

	public override void CustomEvent (){
		BLE.Instance.Manager.SetUpcomingPayload (upcomingPayload);
	}

	#if UNITY_EDITOR
	protected override string GetObjectName(){
		return this.GetType ().Name + " - "+upcomingPayload;
	}
	#endif

}