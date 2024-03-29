﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class ActorDisplays : MonoBehaviour{

	public Slider progressSlider;
	public Slider[] markerSliders;

	public Text statusText;

	public ActorPlayer actorPlayer;

	public Text actorName;
	public Text actName;

	private AudioSource assistantSoundSource;

	private ActorActSet currentActorSet;

	public SignatureColourDisplay[] ignoredDisplays;

	public SignatureColourDisplay[] currentGroupDisplays;
	public TextToSignatureString[] currentGroupSignatureTexts;

	public Button cancelChooseActorButton;

	public GameObject loadingNote;

	public TracklistPlayer player;

	public Slider scrubSlider;
	private bool scrubDown = false;

	public bool playerWasPlaying = false;

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
		actorPlayer.ActingToNewGroupEvent += NewGroup;
	}
	private void OnDisable(){
		actorPlayer.ActorChangedEvent -= ActorChanged;
		actorPlayer.ActorBeginsActEvent -= ActorBeganAct;
		actorPlayer.ActorEndsEvent -= ActorComplete;
		actorPlayer.IgnoredClearedEvent -= ClearedIgnored;
		actorPlayer.ActingToNewGroupEvent -= NewGroup;
	}

	private void IgnoredSignal(Signal s){
		for (int k = 0; k < ignoredDisplays.Length; k++) {
			if (ignoredDisplays [k].signature == s.GetSignature ()) {
				ignoredDisplays [k].SetColourVisible (true);
			}
		}
	}

	private void ClearedIgnored(){
		for (int k = 0; k < ignoredDisplays.Length; k++) {
			ignoredDisplays [k].SetColourVisible (false);
		}
	}

	private void NewGroup(Signature s){
		for (int k = 0; k < currentGroupDisplays.Length; k++) {
			if (currentGroupDisplays [k].signature == s) {
				currentGroupDisplays [k].SetColourVisible (true);
			} else {
				currentGroupDisplays [k].SetColourVisible (false);
			}
		}
		for (int k = 0; k < currentGroupSignatureTexts.Length; k++) {
			currentGroupSignatureTexts[k].UpdateValue (s);
		}
	}

	private void ActorChanged(ActorActSet aas){
		Diglbug.Log ("Actor controls changing display values to " + aas.name, PrintStream.ACTORS);
		if (currentActorSet != null) {
			currentActorSet.ActContentCompleteEvent -= MarkerComplete;
			currentActorSet.WaitingForNextActEvent -= WaitingForNextAct;
		}
		currentActorSet = aas;
		currentActorSet.ActContentCompleteEvent += MarkerComplete;
		currentActorSet.WaitingForNextActEvent += WaitingForNextAct;


		SetMarkersFromActorActSet (currentActorSet);
		if (Variables.Instance.language == Language.ENGLISH) {
			actorName.text = EnumDisplayNamesEnglish.ActorName (currentActorSet.actor);
			actName.text = EnumDisplayNamesEnglish.DefinedActName (aas.GetFirstAct ().definedAct);
		} else {
			actorName.text = EnumDisplayNamesMandarin.ActorName (currentActorSet.actor);
			actName.text = EnumDisplayNamesMandarin.DefinedActName (aas.GetFirstAct ().definedAct);
		}
		SetWaiting ();
		assistantSoundSource.Play ();
		cancelChooseActorButton.gameObject.SetActive (true);
	}

	private void SetMarkersFromActorActSet(ActorActSet aas){

		float[] markerPositions = aas.GetActMarkerPositions ();

		float accum = 0f;
		for (int k = 0; k < markerSliders.Length; k++) {
			if (k < markerPositions.Length) {
				accum += markerPositions [k];
				markerSliders [k].value = accum;
				markerSliders [k].gameObject.SetActive (true);
			} else {
				markerSliders [k].gameObject.SetActive (false);
			}
		}
	}

	private void SetWaiting(){
		statusText.text = Variables.Instance.language == Language.ENGLISH ? "Waiting for scene to begin…" : "等待下一幕，请稍后";
	}

	private void ActorBeganAct(Act a){

		SetActName (a);
		statusText.text = Variables.Instance.language == Language.ENGLISH ? "Scene underway." : "正在演出";
	}

	private void SetActName(Act a){
		if (Variables.Instance.language == Language.ENGLISH) {
			actName.text = EnumDisplayNamesEnglish.DefinedActName(a.definedAct);
		} else {
			actName.text = EnumDisplayNamesMandarin.DefinedActName(a.definedAct);
		}
	}

	private void ActorComplete(ActorActSet a){

	}

	private void MarkerComplete(int index){
		SetWaiting();
	}

	private void WaitingForNextAct(ActorAct aa){
		SetActName (aa);
	}

	private void Update(){
		if (currentActorSet != null) {
			float cProg = currentActorSet.GetActingProgress ();
			progressSlider.value = cProg;
			loadingNote.SetActive (!player.GetTrack ().IsLoaded ());

			if (scrubDown) {
				// wait up
			} else {
				scrubSlider.value = cProg;
			}
			if (!playerWasPlaying && player.IsPlaying ()) {
				assistantSoundSource.Stop ();
			}
			if (playerWasPlaying && !player.IsPlaying ()) {
				if (!scrubDown) {
					assistantSoundSource.Play ();
				}
			}
			playerWasPlaying = player.IsPlaying ();
		}
	}



	public void ScrubPressed(){
		scrubDown = true;
		player.Pause ();
	}

	public void ScrubReleased(){
		scrubDown = false;
		player.Unpause ();

		float skipAmount = scrubSlider.value;
		actorPlayer.Rehearse_SkipToProgress (scrubSlider.value);
	}

}
