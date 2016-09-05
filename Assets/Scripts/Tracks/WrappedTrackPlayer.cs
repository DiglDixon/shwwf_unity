using UnityEngine.Audio;

// There has to be an easier way to pip these...

/// <summary>
/// Pipes all TrackOutput functions to an external TrackOutput object
/// </summary>
public abstract class WrappedTrackOutput : TrackOutput{

	protected abstract TrackOutput WrappedOutput{
		get;
	}

	public override void SetTrackProgress (float p){
		WrappedOutput.SetTrackProgress (p);
	}

	public override void SetTrackTime (float seconds){
		WrappedOutput.SetTrackTime (seconds);
	}

	public override void SetSourceTime(float time){
		WrappedOutput.SetSourceTime (time);
	}

	public override float GetProgress (){
		return WrappedOutput.GetProgress ();
	}

	public override void Stop(){
		WrappedOutput.Stop ();
	}

	public override bool IsPlaying (){
		return WrappedOutput.IsPlaying();
	}

	public override void FadeIn(float time){
		WrappedOutput.FadeIn (time);
	}

	public override void FadeOut(float time){
		WrappedOutput.FadeOut (time);
	}

	public override void SetMixerGroup(AudioMixerGroup amg){
		WrappedOutput.SetMixerGroup (amg);
	}

	public override float GetTimeRemaining(){
		return WrappedOutput.GetTimeRemaining ();
	}
	public override float GetTimeElapsed(){
		return WrappedOutput.GetTimeElapsed ();
	}

	public override void SetTrack(ITrack t){
		WrappedOutput.SetTrack (t);
	}

	public override ITrack GetTrack(){
		return WrappedOutput.GetTrack();
	}

	public override void Play (){
		WrappedOutput.Play ();
	}

	public override void Pause (){
		WrappedOutput.Pause ();
	}

	public override void Unpause(){
		WrappedOutput.Unpause ();
	}

	public override void CutIn(){
		WrappedOutput.CutIn ();
	}

	public override void CutOut(){
		WrappedOutput.CutOut ();
	}

}