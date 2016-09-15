using UnityEngine;
using System.Collections;

public class SetRecoveryPlayingOnStart : MonoBehaviour {

	public bool isUnderway = false;

	private void Start(){
		if (isUnderway) {
			RecoveryManager.Instance.ShowUnderway ();
		} else {
			RecoveryManager.Instance.ShowNotUnderway ();
		}
	}

	private void OnValidate(){
		gameObject.name = typeof(SetRecoveryPlayingOnStart).Name+": "+isUnderway;
	}
}
