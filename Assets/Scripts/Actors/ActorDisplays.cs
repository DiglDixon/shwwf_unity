using UnityEngine;
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
		}
		currentActorSet = aas;
		currentActorSet.ActContentCompleteEvent += MarkerComplete;



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
				Diglbug.Log ("Marker set: " + markerPositions [k], PrintStream.DEBUGGING);
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
		if (Variables.Instance.language == Language.ENGLISH) {
			actName.text = EnumDisplayNamesEnglish.DefinedActName(a.definedAct);
		} else {
			actName.text = EnumDisplayNamesMandarin.DefinedActName(a.definedAct);
		}
		assistantSoundSource.Stop ();

		statusText.text = Variables.Instance.language == Language.ENGLISH ? "Scene underway." : "正在演出";
	}

	private void ActorComplete(ActorActSet a){
		// sweet.
//		statusText
	}

//	private IEnumerator RunActorCompleteDisplay(){
//		statusText.text = (Variables.Instance.language == Language.ENGLISH ? "Scene finished! Preparing for next group…" : "本幕完。 准备重新开始");
//		yield return new WaitForSeconds
//	}

	private void MarkerComplete(int index){
//		waitingForNextSceneText.gameObject.SetActive (true);
		SetWaiting();
	}

	private void Update(){
		if (currentActorSet != null) {
			progressSlider.value = currentActorSet.GetActingProgress ();
			loadingNote.SetActive (!player.GetTrack ().IsLoaded ());
		}
	}


}
