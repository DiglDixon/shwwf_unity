using UnityEngine;

public abstract class SceneControls : MonoBehaviour{

	public TracklistPlayer player;
	public ActSet actSet;

	public virtual void BeginExpectedScene(){
		player.SendExpectedActWhenLoaded ();
	}

	public virtual void BeginCustomAct(Act a){
		player.SendCustomActWhenLoaded (a);
	}

}
