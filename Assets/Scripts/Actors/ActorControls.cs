using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]
public class ActorControls : MonoBehaviour{

	public Slider progressSlider;
	public Slider[] markerSliders;

//	public Text waitingForNextSceneText;

	public ActorPlayer actorPlayer;

	public Text actorName;

	private AudioSource assistantSoundSource;

	private ActorActSet currentActorSet;

	private void Awake(){
		assistantSoundSource = GetComponent<AudioSource> ();
	}

	private void OnEnable(){
		actorPlayer.ActorChangedEvent += ActorChanged;
		actorPlayer.ActorBeginsActEvent += ActorBeganAct;
		actorPlayer.ActorEndsEvent += ActorComplete;
	}
	private void OnDisable(){
		actorPlayer.ActorChangedEvent -= ActorChanged;
		actorPlayer.ActorBeginsActEvent -= ActorBeganAct;
		actorPlayer.ActorEndsEvent -= ActorComplete;
	}

	private void ActorChanged(ActorActSet aas){
		Diglbug.Log ("Actor controls changing display values to " + aas.name, PrintStream.ACTORS);
		if (currentActorSet != null) {
			currentActorSet.ActContentCompleteEvent -= MarkerComplete;
		}
		currentActorSet = aas;
		currentActorSet.ActContentCompleteEvent += MarkerComplete;
		actorName.text = currentActorSet.actor.ToString ();

		float[] markerPositions = currentActorSet.GetActMarkerPositions ();

		float accum = 0f;
		for (int k = 0; k < markerSliders.Length; k++) {
			if (k < markerPositions.Length) {
				Diglbug.Log ("Marker set: " + markerPositions [k], PrintStream.DEBUGGING);
				accum += markerPositions [k];
				markerSliders [k].value = accum;
			} else {
				markerSliders [k].gameObject.SetActive (false);
			}
		}

		assistantSoundSource.Play ();
	}

	private void ActorBeganAct(Act a){
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
