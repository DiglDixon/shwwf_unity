﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]
public class ActorDisplays : MonoBehaviour{

	public Slider progressSlider;
	public Slider[] markerSliders;

//	public Text waitingForNextSceneText;

	public ActorPlayer actorPlayer;

	public Text actorName;
	public Text actName;

	private AudioSource assistantSoundSource;

	private ActorActSet currentActorSet;

	public IgnoredSignatureDisplay[] ignoredDisplays;

	private void Awake(){
		assistantSoundSource = GetComponent<AudioSource> ();
		ClearedIgnored ();
	}

	private void OnEnable(){
		actorPlayer.ActorChangedEvent += ActorChanged;
		actorPlayer.ActorBeginsActEvent += ActorBeganAct;
		actorPlayer.ActorEndsEvent += ActorComplete;
		actorPlayer.SignalIgnoreAddedEvent += IgnoredSignal;
		actorPlayer.IgnoredClearedEvent += ClearedIgnored;
	}
	private void OnDisable(){
		actorPlayer.ActorChangedEvent -= ActorChanged;
		actorPlayer.ActorBeginsActEvent -= ActorBeganAct;
		actorPlayer.ActorEndsEvent -= ActorComplete;
		actorPlayer.IgnoredClearedEvent -= ClearedIgnored;
	}

	private void IgnoredSignal(Signal s){
		for (int k = 0; k < ignoredDisplays.Length; k++) {
			if (ignoredDisplays [k].signature == s.GetSignature ()) {
				ignoredDisplays [k].SetIgnored (true);
			}
		}
	}

	private void ClearedIgnored(){
		for (int k = 0; k < ignoredDisplays.Length; k++) {
			ignoredDisplays [k].SetIgnored (false);
		}
	}

	private void ActorChanged(ActorActSet aas){
		Diglbug.Log ("Actor controls changing display values to " + aas.name, PrintStream.ACTORS);
		if (currentActorSet != null) {
			currentActorSet.ActContentCompleteEvent -= MarkerComplete;
		}
		currentActorSet = aas;
		currentActorSet.ActContentCompleteEvent += MarkerComplete;
		actorName.text = currentActorSet.actor.ToString ();

		SetMarkersFromActorActSet (currentActorSet);

		assistantSoundSource.Play ();
	}

	private void SetMarkersFromActorActSet(ActorActSet aas){

		float[] markerPositions = aas.GetActMarkerPositions ();

		float accum = 0f;
		for (int k = 0; k < markerSliders.Length; k++) {
			if (k < markerPositions.Length) {
				Diglbug.Log ("Marker set: " + markerPositions [k], PrintStream.DEBUGGING);
				accum += markerPositions [k];
				markerSliders [k].value = accum;
				markerSliders [k].gameObject.SetActive (true);
			} else {
				markerSliders [k].gameObject.SetActive (false);
			}
		}
	}

	private void ActorBeganAct(Act a){
		actName.text = a.name; // TODO some sort of display name.
		assistantSoundSource.Stop ();
	}

	private void ActorComplete(ActorActSet a){
		// sweet.
	}

	private void MarkerComplete(int index){
		Diglbug.Log ("MARKER " + index + " COMPLETE!");
//		waitingForNextSceneText.gameObject.SetActive (true);
	}

	private void Update(){
		if (currentActorSet != null) {
			progressSlider.value = currentActorSet.GetActingProgress ();
		}
	}


}
