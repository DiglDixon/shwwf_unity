using UnityEngine;
using UnityEngine.Audio;


public class LerpAudioMixerParameter : LerpFloat{

	private string mixerParameterName;
	private AudioMixer mixer;

	public LerpAudioMixerParameter(AudioMixer mixer, string mixerParameterName){
		this.mixer = mixer;
		this.mixerParameterName = mixerParameterName;
	}

	protected override void LerpStepValue (float value){
		mixer.SetFloat (mixerParameterName, -80f+80f*value);
	}

}