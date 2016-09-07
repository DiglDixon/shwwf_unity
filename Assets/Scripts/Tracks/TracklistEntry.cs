using UnityEngine;
using UnityEngine.UI;

public abstract class TracklistEntry : ListEntry{
	
	protected ITrack track;

	public abstract void FetchTrack ();

	public virtual void Initialise (){
		// optional
	}
	public virtual ITrack GetTrack(){
		return track;
	}

	public virtual float GetTrackLength(){
		return GetTrack ().GetTrackLength ();
	}

	public string GetDisplayName(){
		return track.GetTrackName ();
	}

	public float GetEntranceFadeTime(){
		return track.EntranceFadeTime ();
	}

}