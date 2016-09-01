using UnityEngine;

public class Tracklist : MonoBehaviour{

	public TracklistEntry[] entries{ get; private set; }

	void Awake(){
		entries = GetComponentsInChildren<TracklistEntry> ();
		InitialiseEntries ();
	}

	private void InitialiseEntries(){
		for (int k = 0; k < entries.Length; k++) {
			entries [k].LoadTrack ();
		}
	}

	public TracklistEntry GetTrackEntryAtIndex(int i){
		if (i >= entries.Length)
			return null;
		return entries [i];
	}

}