
public class CreditsEvent : CustomTrackTimeEvent{

	public CreditsSequence credits;
	public bool begin;

	public override void CustomEvent (){
		if (begin) {
			credits.Begin ();
		} else {
			credits.Cancel ();
		}
	}

	protected override string GetObjectName (){
		return (begin? "begin_" : "end_")+base.GetObjectName ();
	}

}