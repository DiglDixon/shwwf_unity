
public class StopPeriodicVibrateEvnt : CustomTrackTimeEvent{

	public PeriodicVibrateEvent toStop;

	public override void CustomEvent (){
		toStop.StopVibration ();
	}

}