public class RegionTimingEvent {

	public EventTrack.TrackEventDelegate startFunction;
	public EventTrack.TrackEventDelegate endFunction;
	public EventTrack.TrackEventDelegate skipFunction;
	public float startTime;
	public float endTime;
	public string eventName;

	public RegionTimingEvent(CustomRegionTrackTimeEvent e){
		
		this.startFunction = e.CustomEvent;
		this.endFunction = e.CustomExitRegionEvent;
		this.skipFunction = e.SkipRegionEvent;
		this.startTime = e.occurAtTime;
		this.endTime = e.GetRegionEndTime();
		this.eventName = e.gameObject.name;
	}
}