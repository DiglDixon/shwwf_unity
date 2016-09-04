using UnityEngine;

public class Tracklist : MonoBehaviour{

	public TracklistEntry[] entries{ get; private set; }

	#if UNITY_EDITOR
	public bool updateName;
	#endif

	void Awake(){
		entries = GetComponentsInChildren<TracklistEntry> ();
		InitialiseEntries ();
	}

//	public void AddTimeRemainingEventToEntries<T>(EventTrack.TrackEventDelegate function, float time){
////		for(int k = 0;
//	}

	private void InitialiseEntries(){
		for (int k = 0; k < entries.Length; k++) {
			entries [k].FetchTrack ();
			entries [k].Initialise ();
		}
	}

	public TracklistEntry GetTrackEntryAtIndex(int i){
		if (i >= entries.Length)
			return null;
		return entries [i];
	}

	#if UNITY_EDITOR
	private void OnValidate(){
		updateName = false;
		EventTrack[] eventTracks = GetComponentsInChildren<EventTrack> ();
		foreach (EventTrack et in eventTracks) {
			et.UpdateName ();
		}
	}
	#endif

}