
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(DesktopVideoTrack))]
[RequireComponent (typeof(MobileVideoTrack))]
public class VideoTracklistEntry : TracklistEntry{

	public override void LoadTrack(){
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

	public override bool Looping (){
		return false;
	}

}