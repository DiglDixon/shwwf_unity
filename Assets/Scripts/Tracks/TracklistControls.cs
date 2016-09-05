using UnityEngine;
using UnityEngine.UI;

public class TracklistControls : MonoBehaviour{
	
	public Tracklist tracklist;
	public TracklistPlayer player;

	public void TracklistItemChosen(int i){
		TracklistEntry toPlay = GetEntryAtIndex (i);
		player.PlayTrackEntry (toPlay);
	}

	public TracklistEntry GetEntryAtIndex(int i){
		return tracklist.GetTrackEntryAtIndex (i);;
	}


}