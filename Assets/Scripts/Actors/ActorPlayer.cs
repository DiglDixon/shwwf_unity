using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(EnsureActorSets))]
public class ActorPlayer : MonoBehaviour{

	public ActorActSet currentActorSet{ get; private set;}
	private ActorActSet[] actorSets;

	public AudioSource sfxSource;
	public AudioClip sceneBeginsSound;

	public TracklistPlayer player;

	private List<Signal> ignoredSignals = new List<Signal> ();

	public PayloadEventSystem eventSystem;

	public delegate void ActorChangedDelegate(ActorActSet aas);
	public event ActorChangedDelegate ActorChangedEvent;

	public delegate void ActorBeginsActDelegate(Act act);
	public event ActorBeginsActDelegate ActorBeginsActEvent;

	public delegate void ActorEndsDelegate(ActorActSet aas);
	public event ActorEndsDelegate ActorEndsEvent;

	public delegate void IgnoredClearedDelegate();
	public event IgnoredClearedDelegate IgnoredClearedEvent;

	public delegate void SignalIgnoredDelegate(Signal s);
	public event SignalIgnoredDelegate SignalIgnoreAddedEvent;

	public delegate void ActingToNewGroupDelegate(Signature s);
	public event ActingToNewGroupDelegate ActingToNewGroupEvent;

	private Signature currentGroupActingTo;

	private void Awake(){
		actorSets = GetComponentsInChildren<ActorActSet>();
		eventSystem.ExternallyDefinedEvent += SignalReceived;
	}

//	private IEnumerator Start(){
//		yield return new WaitForSeconds (1f);
////		SetActor (currentActorSet.actor);
//	}

	public void SetActor(Actor actor){
		player.ClearPreservedTracks ();
		currentActorSet = actorSets [(int)actor];
		currentActorSet.InitialiseSet ();
		ResetCurrentActor ();
		if (ActorChangedEvent != null) {
			ActorChangedEvent (currentActorSet);
		}
		player.AddPreservedTrack (currentActorSet.GetFirstTrackEntry ().GetTrack ());
	}

	private void ResetCurrentActor(){
		SetCurrentGroup(Signature.NONE);
		ITrack t = currentActorSet.GetFirstTrackEntry ().GetTrack ();
		player.SetWaitingTrackEntry (t);
	}

	public void CancelAct(){
		ResetCurrentActor ();
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
			if (currentGroupActingTo == Signature.NONE || currentGroupActingTo == s.GetSignature ()) {
				if (SignalIsIgnored (s)) {
					Diglbug.Log ("Rejected signal " + s.GetPrint () + " as it it's ignored", PrintStream.ACTORS);
				} else {
					Act actToBegin = GetActSignalStarts (s);
					if (actToBegin != null) {
						BeginAct (actToBegin, s);
						SetCurrentGroup (s.GetSignature ());
						AddIgnoredSignal (s); // cache this here so we don't re-trigger if a foreign signal jockeys us.
						sfxSource.PlayOneShot(sceneBeginsSound, 0.5f);
						Diglbug.Log ("Accepted new signal " + s.GetPrint ());
					} else {
						Diglbug.Log ("Rejected signal " + s.GetPrint () + " and it's not relevant for this actor", PrintStream.ACTORS);
					}
				}
			} else {
				Diglbug.Log ("Bounced a foreign group signal");
			}
		}
	}

	private void BeginAct(Act actToBegin, Signal s){
//		actToBegin.Begin ();
		player.BeginActFromSignal (actToBegin, s);
		if (ActorBeginsActEvent != null) {
			ActorBeginsActEvent (actToBegin);
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
		if (IgnoredClearedEvent != null) {
			IgnoredClearedEvent ();
		}
	}

	public int PreviousSignalsCount(){
		return ignoredSignals.Count;
	}

	public void ActorSetFinished(ActorActSet set){
		Diglbug.Log ("Actor Set Finished, should reinitialise now. " + set.name);
		ResetCurrentActor ();
		if (ActorEndsEvent != null) {
			ActorEndsEvent (set);
		}
	}

	private void AddIgnoredSignal(Signal s){
		ignoredSignals.Add (s);
		if (SignalIgnoreAddedEvent != null) {
			SignalIgnoreAddedEvent (s);
		}

	}

	private void SetCurrentGroup(Signature s){
		currentGroupActingTo = s;
		if (ActingToNewGroupEvent != null) {
			ActingToNewGroupEvent (s);
		}
	}

	public void Rehearse_Play(){
		BeginAct (currentActorSet.GetFirstAct (), SignalUtils.NullSignal);
	}

	public void Rehearse_PlayNextAct(){
		ActorAct next = currentActorSet.Rehearsal_GetNextAct ();
		if (next == null) {
			// rehearsal is done
			Rehearse_Stop();
		} else {
			BeginAct (next, SignalUtils.NullSignal);
		}

	}

	public void Rehearse_Stop(){
		ActorSetFinished(currentActorSet);
	}
}