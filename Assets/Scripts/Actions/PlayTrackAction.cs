using UnityEngine;

public class PlayTrackAction : PayloadEventAction{

	public TracklistPlayer player;
	public TracklistEntry trackEntry;

	public override void FireEvent (Signal s){
		player.PlayTrackEntry (trackEntry);
		int offset = SignalUtils.GetSignalTimeOffset (s.GetSignalTime ());
		Diglbug.Log ("Skipping track by " + offset + " seconds due to latency", PrintStream.AUDIO_PLAYBACK);
		Diglbug.LogMobile ("LATSKIP:" + offset, "SKIPS");
		player.SetTrackTime (offset);
	}

}