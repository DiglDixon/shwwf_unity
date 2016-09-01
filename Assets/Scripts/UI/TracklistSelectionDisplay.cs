using UnityEngine;

public class TracklistSelectionDisplay : ListDisplay{
	
	public TracklistControls tracklistControls;

	void Awake(){
		ContructUIFromTracklist (tracklistControls.tracklist);
	}

	private void ContructUIFromTracklist(Tracklist tl){
		for (int k = 0; k < tl.entries.Length; k++) {
			AddListItem (tl.entries [k]);
		}
	}

	public override void ItemPressed (int itemIndex){
		base.ItemPressed (itemIndex);
		tracklistControls.TracklistItemChosen (itemIndex);
		Close ();
	}
}