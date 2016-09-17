
public class LoadVideoEvent : CustomTrackTimeEvent{

	public VideoTracklistEntry videoToLoad;
	public TracklistPlayer player;

	public override void CustomEvent (){
		player.LoadTrackIfNeeded (videoToLoad.GetTrack ());
//		videoToLoad.GetTrack().Load();
	}

	protected override string GetObjectName ()
	{
		return GetTimeAtString()+" Load "+(videoToLoad!=null? videoToLoad.name : "UNDEFINED");
	}

}