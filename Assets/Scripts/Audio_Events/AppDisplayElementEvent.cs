
public class AppDisplayElementEvent : CustomTrackTimeEvent{

	public AppDisplayElement element;
	public bool enter;

	public override void CustomEvent (){
		if (enter)
			element.Enter ();
		else
			element.Exit ();
	}

	protected override string GetNameDetails (){
		return (enter ? "ENTER_" : "EXIT_") + (element == null ? "UNDEF" : element.name);
	}

}