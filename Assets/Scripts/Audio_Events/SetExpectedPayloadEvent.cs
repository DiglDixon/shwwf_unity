
public class SetExpectedPayloadEvent : CustomTrackTimeEvent{

	public Payload expectedPayload;

	public override void CustomEvent (){
		Diglbug.Log ("FIRING TEST CUSTOM EVENT SETEXPECTEDTRIGGEREVENT");
	}

}