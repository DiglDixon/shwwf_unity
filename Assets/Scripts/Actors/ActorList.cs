using UnityEngine;

public class ActorList : MonoBehaviour {

	private ActorEntry[] actors;
	public void Awake(){
		actors = GetComponentsInChildren<ActorEntry> ();
	}

}

