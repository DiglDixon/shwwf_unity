
public class PrintMessageTrackEvent : CustomTrackTimeEvent{

	public string message;
	public string mobileChannel;

	public override void CustomEvent (){
		Diglbug.Log (message, PrintStream.DEBUGGING);
		Diglbug.LogMobile (message, mobileChannel);
	}

}