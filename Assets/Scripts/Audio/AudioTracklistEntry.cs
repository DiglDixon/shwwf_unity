using UnityEngine;

[RequireComponent (typeof(AudioTrack))]
public class AudioTracklistEntry : TracklistEntry{
	
	public override void LoadTrack(){
		track = GetComponent<AudioTrack> ();
	}

	public override GameObject ConstructListObject (){
		GameObject ret = GameObject.Instantiate(Resources.Load("Tracklist_Entry_List_Item")) as GameObject;
		TracklistEntryListDisplayItem display = ret.GetComponent<TracklistEntryListDisplayItem> ();
		display.SetDisplayFromTrack (track);
		return ret;
	}

}