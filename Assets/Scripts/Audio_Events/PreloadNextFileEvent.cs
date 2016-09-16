
public class PreloadNextFileEvent : CustomTrackTimeEvent{

	public TracklistPlayer tracklistPlayer;
	public int indicesAhead = 2;

	public override void CustomEvent (){
		tracklistPlayer.LoadNextTrack (indicesAhead);
	}

	#if UNITY_EDITOR
	protected override string GetObjectName(){
		return this.GetType ().Name + " - "+indicesAhead;
	}
	#endif

}