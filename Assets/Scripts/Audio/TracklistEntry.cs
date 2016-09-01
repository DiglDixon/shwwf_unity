using UnityEngine;
using UnityEngine.UI;

public abstract class TracklistEntry : ListEntry{
	
	protected ITrack track;

	public abstract void AssignTrack ();

	public virtual ITrack GetTrack(){
		return track;
	}

	public string GetDisplayName(){
		return track.GetTrackName () + (Looping() ? " (looping)" : "");
	}

	public float GetEntranceFadeTime(){
		return track.FadeTime ();
	}

	public virtual bool Looping(){
		return false;
	}

}