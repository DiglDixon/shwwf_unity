using UnityEngine;
using System.Collections.Generic;


public class ActorPlayer : EnsureActorSetsInChildren<ActorActSet>{

	public ActorActSet currentActorSet;
	private ActorActSet[] actorSets;

	public TracklistPlayer player;

	private List<Signal> ignoredSignals = new List<Signal> ();

	public PayloadEventSystem eventSystem;


	private void Awake(){
		actorSets = GetComponentsInChildren<ActorActSet>();
	}

	private void Start(){
		eventSystem.ExternallyDefinedEvent += SignalReceived;
		SetActor (currentActorSet.actor);
	}

	public void SetActor(Actor actor){
		currentActorSet = actorSets [(int)actor];
		player.PrepareTrack (currentActorSet.GetFirstTrackEntry ());
	}

	private void OnEnable(){
		if (actorSets == null) {
			actorSets = GetComponentsInChildren<ActorActSet>();
		}
		for (int k = 0; k< actorSets.Length; k++) {
			actorSets[k].SetCompleteEvent += ActorSetFinished;
		}
	}

	private void OnDisable(){
		for (int k = 0; k< actorSets.Length; k++) {
			actorSets[k].SetCompleteEvent += ActorSetFinished;
		}
	}

	public void SignalReceived(Signal s){
		if (currentActorSet != null) {
			
			if (SignalIsIgnored (s)) {
				Diglbug.Log ("Rejected signal "+s.GetPrint()+" as it it's ignored", PrintStream.ACTORS);
			} else {
				
				Act actToBegin = GetActSignalStarts (s);
				if (actToBegin != null) {

					actToBegin.Begin ();
					ignoredSignals.Add (s); // cache this here so we don't re-trigger if a foreign signal jockeys us.
					Diglbug.Log ("Accepted new sign");

				} else {
					Diglbug.Log ("Rejected signal "+s.GetPrint()+" and it's not relevant for this actor", PrintStream.ACTORS);
				}
			}
		}
	}

	private bool SignalIsIgnored(Signal s){
		for (int k = 0; k < ignoredSignals.Count; k++) {
			if (ignoredSignals[k].Equals(s) ) { // this check includes signature and payload.
				return true;
			}
		}
		return false;
	}

	private Act GetActSignalStarts(Signal s){
		return currentActorSet.GetActPayloadStarts (s.GetPayload ());
	}

	public void ClearPreviousSignals(){
		Diglbug.Log ("Clearing " + ignoredSignals.Count+" previous signals...", PrintStream.ACTORS);
		ignoredSignals.Clear ();
	}

	public void ChangeActor(Actor a){
		player.Stop();
		currentActorSet = actorSets[(int)a];
	}

	public void ActorSetFinished(ActorActSet set){
		Diglbug.Log ("Actor Set Finished, should reinitialise now. " + set.name);
	}

}