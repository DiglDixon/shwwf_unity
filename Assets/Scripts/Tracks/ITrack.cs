﻿using UnityEngine;

public interface ITrack{
	string GetTrackName ();
	float GetInverseTrackLength();
	float GetInverseTrackFrequency();
	float GetTrackLength();
	bool IsLoaded();
	bool IsLoading();
	void Load();
	void Unload();
	float EntranceFadeTime(); // TODO: This is a little awkward - we may need different fade times for difference situations.
//	AudioClip GetAudioClip();
}