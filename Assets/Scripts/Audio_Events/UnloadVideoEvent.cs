
public class UnloadVideoEvent : CustomTrackTimeEvent{

	public VideoTracklistEntry videoToUnload;
	public TracklistPlayer player;

	public override void CustomEvent (){
		player.UnloadTrack (videoToUnload.GetTrack ());//.Unload ();
		videoToUnload.GetTrack().Unload();
	}

}