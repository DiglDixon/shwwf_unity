

public abstract class SingleRegionCustomEvent : CustomRegionTrackTimeEvent{
	
	public float regionEndTime; // we only support times from the start, at the moment.

	public override float GetRegionEndTime (){
		return regionEndTime;
	}
}