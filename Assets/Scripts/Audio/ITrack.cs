using UnityEngine;

public interface ITrack{
	string GetTrackName ();
	float GetInverseTrackLength();
	float GetInverseTrackFrequency();
	float GetTrackLength();
	bool IsLoaded();
	void Load();
	void Unload();
	float FadeTime(); // TODO: This is a little awkward - we may need different fade times for difference situations.
//	AudioClip GetAudioClip();
}