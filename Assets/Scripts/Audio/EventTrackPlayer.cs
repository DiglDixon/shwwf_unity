

public abstract class EventTrackPlayer : TrackOutput{
	
	private EventTrack eventTrack;

	public override void SetTrack (ITrack newTrack){
		base.SetTrack (newTrack);
		if (newTrack is EventTrack) {
			this.eventTrack = (EventTrack) newTrack;
		} else {
			this.eventTrack = null;
		}
	}

	public override void SetSourceTime(float newTime){
		if (eventTrack) {
			eventTrack.SetTimeElapsed (newTime);
		}
	}

	protected void Update (){
		if (eventTrack != null) {
			eventTrack.UpdateTimeElapsed (GetTimeElapsed ());
		}
	}
}
