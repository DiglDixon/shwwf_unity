using UnityEngine;
using UnityEngine.Audio;

public class ModifyAudioMixerForEditor : MonoBehaviour{

	public AudioMixer audioMixer;
	public string param = "";
	public float value = 0f;
	#if UNITY_EDITOR
	void Start(){
		audioMixer.SetFloat (param, value);
	}
	#endif

}
