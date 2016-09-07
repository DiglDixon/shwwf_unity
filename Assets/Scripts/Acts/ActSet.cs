﻿using UnityEngine;

public class ActSet : EnsurePayloadsInChildren<Act>{

	private Act[] acts = new Act[0];

	public Act startingAct;
	public Act endingAct;

	private Act currentAct;

	public TracklistPlayer player;

	public delegate void ActChangedDelegate(Act newAct);
	public event ActChangedDelegate ActChangedEvent;

	private void Awake(){
		acts = GetComponentsInChildren<Act> ();
	}

	private void OnEnable(){
		player.NewTrackBeginsEvent += TrackBegins;
	}

	private void OnDisable(){
		player.NewTrackBeginsEvent -= TrackBegins;
	}

	public void TrackBegins(ITrack track){
		for (int k = 0; k < acts.Length; k++) {
			if (acts [k].ContainsTrack (track)) {
				TrackBeganInAct (track, acts [k]);
			}
		}
	}

	private void TrackBeganInAct(ITrack track, Act act){
		if (act != currentAct) {
			ChangeAct(act);
		}

	}

	private void ChangeAct(Act act){
		currentAct = act;
		currentAct.Begin ();
		if (ActChangedEvent != null) {
			ActChangedEvent (currentAct);
		}
	}

}