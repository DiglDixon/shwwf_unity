
public abstract class CustomRegionTrackTimeEvent : CustomTrackTimeEvent{

	public abstract float GetRegionEndTime ();

	protected override string GetObjectPrefix ()
	{
		return "R-"+base.GetObjectPrefix ();
	}

	protected override string GetTimeAtString ()
	{
		return base.GetTimeAtString ()+"->"+GetRegionEndTime();
	}

	public abstract void CustomExitRegionEvent ();

	public virtual void SkipRegionEvent (){
		// empty, but full of potential ;)
	}

}