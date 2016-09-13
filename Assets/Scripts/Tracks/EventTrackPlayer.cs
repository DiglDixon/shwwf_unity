

public abstract class EventTrackPlayer : AbstractTrackPlayer{
	
	private EventTrack eventTrack;

	public override void Play(){
		EnableEvents ();
	}

	public override void Stop(){
		DisableEvents ();
	}

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

	protected override void Update (){
		base.Update();
		if (IsPlaying ()) {
			if (eventTrack != null) {
				eventTrack.UpdateTimeElapsed (GetTimeElapsed ());
			}
		}
	}

	public override void Skipped (){
		FireRemainingStateEvents ();
		base.TrackReachedEnd ();
	}

	public void FireRemainingStateEvents(){
		Diglbug.Log ("Firing remaining state delegates "+name, PrintStream.DELEGATES);
		eventTrack.SetTimeElapsed (GetTrack ().GetTrackLength());
	}

	public void FireRemainingEvents(){
		Diglbug.Log ("Firing remaining delegates "+name, PrintStream.DELEGATES);
		eventTrack.UpdateTimeElapsed (GetTrack ().GetTrackLength());
	}

	protected override void TrackReachedEnd (){
		FireRemainingEvents();
		base.TrackReachedEnd ();
	}

	protected void EnableEvents(){
		if (eventTrack != null) {
			eventTrack.EnableEvents ();
		}
	}

	protected void DisableEvents(){
		if (eventTrack != null) {
			eventTrack.DisableEvents ();
		}
	}
}
