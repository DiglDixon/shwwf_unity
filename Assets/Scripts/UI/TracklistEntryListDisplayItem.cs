using UnityEngine.UI;


public class TracklistEntryListDisplayItem : ListDisplayItem{
	public Text titleText;
	public Text subtitleText;

	public void SetDisplayFromTrack(ITrack t){
		titleText.text = t.GetTrackName();
		subtitleText.text = Utils.AudioTimeFormat(t.GetTrackLength ());
	}

}