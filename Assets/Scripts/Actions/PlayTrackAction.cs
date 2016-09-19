using UnityEngine;

public class PlayTrackAction : PayloadEventAction{

	public TracklistPlayer player;
	public TracklistEntry trackEntry;

	protected override string GetGameObjectName (){
		if (trackEntry != null) {
			AbstractTrack track = trackEntry.gameObject.GetComponent<AbstractTrack> ();
			if(track){
				return "Play " + track.GetTrackName ();
			}
		}
		return "undefined_PlayTrack";
	}

	public override void FireEvent (Signal s){
		player.PlayTrackEntry (trackEntry);
		int offset = SignalUtils.GetSignalTimeOffset (s.GetSignalTime ());
		Diglbug.Log ("Skipping track by " + offset + " seconds due to latency", PrintStream.AUDIO_PLAYBACK);
		player.SetTrackTime (offset);
	}

}