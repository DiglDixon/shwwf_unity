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
	public abstract bool IsLoading();
	public void Load(){
		if (ShouldLoad ()) {
			RunLoad ();
			Diglbug.Log ("Loading track " + GetTrackName(), PrintStream.MEDIA_LOAD);
		} else {
			Diglbug.Log ("Skipping load of already-loaded/ing track " + GetTrackName(), PrintStream.MEDIA_LOAD);
		}
	}
	public void Unload(){
		if (ShouldUnload ()) {
			RunUnload ();
			Diglbug.Log ("Unloading track " + GetTrackName (), PrintStream.MEDIA_LOAD);
		} else {
			Diglbug.Log ("Skipping unload of already-unloaded track " + GetTrackName (), PrintStream.MEDIA_LOAD);
		}
	}
	protected abstract bool ShouldLoad();
	protected abstract bool ShouldUnload();
	protected abstract void RunUnload();
	protected abstract void RunLoad();
	public abstract float FadeTime();

}
