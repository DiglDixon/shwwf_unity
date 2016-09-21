using UnityEngine;
using System.Collections;

public class EmergencyPauseManager : MonoBehaviour {

	public TracklistPlayer player;
	public UILightbox pausedOverlay;

	private bool isPaused = false;


	public void EmergencyPause(){
		BLE.Instance.Manager.ForceSendPayload (Payload.EMERGENCY_PAUSE);
	}

	public void EmergencyUnpause(){
		BLE.Instance.Manager.ForceSendPayload (Payload.EMERGENCY_UNPAUSE);
	}

	public void EmergencyPauseSignalReceived(){
		player.Pause ();
		isPaused = true;
		pausedOverlay.Open ();
	}
		
	public void EmergencyUnpauseSignalReceived(){
		if (isPaused) {
			player.Unpause ();
			isPaused = false;
			pausedOverlay.Close ();
		}
	}

	public void ForceEmergencyUnpause(){
		if (isPaused) {
			player.Unpause ();
//			RecoveryManager.Instance.RecoverFromMostRecentSignal ();
			isPaused = false;
			pausedOverlay.Close ();
		}
	}

}
