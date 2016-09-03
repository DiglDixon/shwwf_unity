using UnityEngine;
using System.Collections;
public class DesktopBluetoothManager : BluetoothManager{

	Signal currentSendingSignal;

	public override void SendSignal (Signal s){
		currentSendingSignal = s;
		StartCoroutine (RunFakeReceiving ());
	}

	public override void StopSending (){
		//
	}

	public override void SetReceiverSignature (Signature s){
		//
	}

	public override void StopReceiving (){
		//
	}

	public override void StartReceiving (){
		//
	}

	private IEnumerator RunFakeReceiving(){
		while (true) {
			FireBeaconFoundEvent (currentSendingSignal);
			yield return new WaitForSeconds (0.5f);
		}
	}


}