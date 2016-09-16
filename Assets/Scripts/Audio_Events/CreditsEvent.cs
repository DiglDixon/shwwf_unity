
public class CreditsEvent : CustomTrackTimeEvent{

	public CreditsSequence credits;

	public override void CustomEvent (){
		credits.Begin ();
	}

}