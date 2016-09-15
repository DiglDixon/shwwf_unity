using UnityEngine;
using System.Collections;

public class ActivateWhenLoading : MonoBehaviour {

	public GameObject toActivate;
	public TracklistPlayer player;

	private void Update(){
		toActivate.SetActive (!player.GetTrack ().IsLoaded());
	}

}
