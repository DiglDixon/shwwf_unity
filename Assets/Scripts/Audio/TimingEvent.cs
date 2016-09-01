
/// <summary>
/// Container class for Track Timing Event-based variables.
/// </summary>
public class TimingEvent{

	public EventTrack.TrackEventDelegate function;
	public float time;

	public TimingEvent(EventTrack.TrackEventDelegate function, float occurOnTimeRemaining){
		this.function = function;
		this.time = occurOnTimeRemaining;
	}
}