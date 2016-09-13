using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public abstract class TrackOutput : MonoBehaviour{

	protected ITrack track = new EmptyTrack ();

	public abstract void SetTrackProgress (float p);
	public abstract void SetTrackTime (float seconds);

	// This should not be public. We should extract this and keep it protected to relevant subsclasses.
	// I can't bring myself to add another subclass yet, though.
	public abstract void SetSourceTime (float time);

	public virtual void SetTrack(ITrack t){
		track = t;
	}

	public virtual ITrack GetTrack(){
		return track;
	}

	public abstract float GetProgress ();

	public abstract void Play ();

	public abstract void Stop();

	public abstract void Pause ();

	public abstract void Unpause ();

	public abstract bool IsPlaying ();

	public abstract bool IsPaused ();

	public abstract void Skipped ();

	public virtual void CutIn(){
		FadeIn (0f);
	}

	public virtual void CutOut(){
		FadeOut (0f);
	}

	public abstract void FadeIn(float time);

	public abstract void FadeOut(float time);

	public abstract void SetMixerGroup(AudioMixerGroup amg);

	public abstract float GetTimeRemaining();
	public abstract float GetTimeElapsed();


}
