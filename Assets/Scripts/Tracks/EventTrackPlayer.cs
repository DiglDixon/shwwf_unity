

public abstract class EventTrackPlayer : AbstractTrackPlayer{
	
	private EventTrack eventTrack;

	private bool eventsEnabled = false;

	public override void Play(){
		base.Play();
		eventTrack.Reset();
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

	public override void SetTrackTime (float seconds){
		Diglbug.Log ("SetTrackTime "+name+", " + seconds, PrintStream.AUDIO_PLAYBACK);
		SetSourceTime(seconds);
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
				if (eventsEnabled){
					eventTrack.UpdateTimeElapsed (GetTimeElapsed ());
				}
			}
		}
	}

	public override void Skipped (){
		FireRemainingStateEvents ();
//		base.TrackReachedEnd ();
	}

	public void FireRemainingStateEvents(){
		Diglbug.Log ("Firing remaining state delegates "+name, PrintStream.DELEGATES);
		eventTrack.SetTimeElapsed (GetTrack ().GetTrackLength());
	}

	public void FireRemainingEvents(){
		Diglbug.Log ("Firing remaining delegates "+name, PrintStream.DELEGATES);
		eventTrack.UpdateTimeElapsed (GetTrack ().GetTrackLength());
	}

//	protected override void TrackReachedEnd (){
////		FireRemainingEvents(); // Think this was busted, and causing big problems.
////		base.TrackReachedEnd ();
//	}

	protected void EnableEvents(){
//		if (eventTrack != null) {
//			eventTrack.EnableEvents ();
//		}
		eventsEnabled = true;
	}

	protected void DisableEvents(){
//		if (eventTrack != null) {
//			eventTrack.DisableEvents ();
//		}
		eventsEnabled = false;
	}
}
