
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
[RequireComponent (typeof(DesktopVideoTrack))]
#endif
[RequireComponent (typeof(MobileVideoTrack))]
public class VideoTracklistEntry : EventTracklistEntry{

	public override void FetchTrack(){
		#if UNITY_EDITOR
		track = GetComponent<DesktopVideoTrack>();
		#else
		track = GetComponent<MobileVideoTrack>();
		#endif
	}

	public override GameObject ConstructListObject (){
		GameObject ret = GameObject.Instantiate (Resources.Load("Tracklist_Entry_List_Item")) as GameObject;
		ret.GetComponent<TracklistEntryListDisplayItem> ().SetDisplayFromTrack (track);
		return ret;
	}

}