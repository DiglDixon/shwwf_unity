using UnityEngine;

/// <summary>
/// An abstract extension of the ITrack interface, using MonoBehaviour
/// </summary>
public abstract class AbstractTrack : MonoBehaviour, ITrack{

	public abstract string GetTrackName ();
	public abstract float GetInverseTrackLength();
	public abstract float GetInverseTrackFrequency();
	public abstract float GetTrackLength();
	public abstract bool IsLoaded();
	public abstract void Load();
	public abstract void Unload();
	public abstract float FadeTime();

}
