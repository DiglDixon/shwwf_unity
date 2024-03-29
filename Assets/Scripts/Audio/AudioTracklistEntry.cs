﻿using UnityEngine;

[RequireComponent (typeof(AudioTrack))]
public class AudioTracklistEntry : EventTracklistEntry{

	public override void FetchTrack(){
		track = GetComponent<AudioTrack> ();
	}

	public override GameObject ConstructListObject (){
		GameObject ret = GameObject.Instantiate(Resources.Load("Tracklist_Entry_List_Item")) as GameObject;
		TracklistEntryListDisplayItem display = ret.GetComponent<TracklistEntryListDisplayItem> ();
		display.SetDisplayFromTrack (track);
		return ret;
	}

}