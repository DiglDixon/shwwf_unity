
public abstract class AppControllerStopStartEvent : AppControllerEvent{

	public bool isStart;

	public override void CustomEvent (){
		if (isStart) {
			FireBeginningEvent ();
		} else {
			FireEndEvent ();
		}
	}

	protected override string GetObjectName ()
	{
		return (isStart? "START_":"END_")+base.GetObjectName ();
	}

	public abstract void FireBeginningEvent();
	public abstract void FireEndEvent();

}