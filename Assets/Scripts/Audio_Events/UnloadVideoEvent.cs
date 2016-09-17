
public class UnloadVideoEvent : CustomTrackTimeEvent{

	public VideoTracklistEntry videoToUnload;
	public TracklistPlayer player;

	public override void CustomEvent (){
		player.UnloadTrack (videoToUnload.GetTrack ());
//		videoToUnload.GetTrack().Unload();
	}

	protected override string GetObjectName ()
	{
		return GetTimeAtString()+" Unload "+(videoToUnload!=null? videoToUnload.name : "UNDEFINED");
	}

}