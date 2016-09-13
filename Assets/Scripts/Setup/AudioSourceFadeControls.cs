using UnityEngine;

[RequireComponent (typeof(LerpableHost))]
[RequireComponent (typeof(AudioSource))]
public class AudioSourceFadeControls : MonoBehaviour{

	private AudioSource source;
	private LerpFloat lerpVolume = new LerpFloat();
	public float volumeFadeTime = 0.5f;
	public float maxVolume = 1f;

	private void Awake(){
		lerpVolume.SubscribeProcess(GetComponent<LerpableHost>());
		lerpVolume.LerpStepValueEvent += LerpVolumeStep;
		lerpVolume.LerpEndsValueEvents += LerpVolumeComplete;
		source = GetComponent<AudioSource> ();
	}

	public void FadeTo(float v){
		if (!source.isPlaying) {
			source.Play ();
		}
		lerpVolume.LerpTo (v * maxVolume, volumeFadeTime);
	}

	public void LoadClip(){
		source.clip.LoadAudioData ();
	}

	private void LerpVolumeStep(float v){
		source.volume = v * maxVolume;
	}

	private void LerpVolumeComplete(float v){
		if (v == 0f) {
			source.Stop ();
		}
	}

}
