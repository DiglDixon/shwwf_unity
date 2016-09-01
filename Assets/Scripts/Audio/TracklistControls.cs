using UnityEngine;
using UnityEngine.UI;

public class TracklistControls : MonoBehaviour{
	
	public Tracklist tracklist;
	public TracklistPlayer player;

	public void TracklistItemChosen(int i){
		player.PlayTracklistFromIndex (tracklist, i);
	}

	public TracklistEntry GetEntryAtIndex(int i){
		return tracklist.GetTrackEntryAtIndex (i);;
	}


}