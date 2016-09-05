using UnityEngine;
using UnityEngine.Audio;

public class MultiTrackOutput : WrappedTrackOutput{

	private int trackOutputIndex = 0;
	public TrackOutput[] trackOutputs;
	protected TrackOutput currentOutput;

	protected override TrackOutput WrappedOutput {
		get {
			return currentOutput;
		}
	}

	private void Awake(){
		SwitchTracks ();
	}

	public void SwitchTracks(){
		currentOutput = GetNextOutput ();
	}

	protected TrackOutput GetNextOutput(){
		trackOutputIndex = (trackOutputIndex + 1) % trackOutputs.Length;
		return trackOutputs [trackOutputIndex];
	}

	public override void SetMixerGroup(AudioMixerGroup mg){
		for (int k = 0; k < trackOutputs.Length; k++) {
			trackOutputs [k].SetMixerGroup (mg);
		}
	}

	public override void Stop(){
		for (int k = 0; k < trackOutputs.Length; k++) {
			trackOutputs [k].Stop ();
		}
	}

	public override void Pause(){
		for (int k = 0; k < trackOutputs.Length; k++) {
			trackOutputs [k].Pause ();
		}
	}

}
