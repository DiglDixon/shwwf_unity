
public class PrintMessageTrackEvent : CustomTrackTimeEvent{

	public string message;

	public override void CustomEvent (){
		Diglbug.Log (message, PrintStream.DEBUGGING);
	}

}