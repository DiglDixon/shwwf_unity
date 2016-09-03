using UnityEngine;

public class BLE : ConstantSingleton<BLE>{

	public BluetoothManager Manager{ get; private set; }

	public delegate void NewSignalFoundDelegate(Signal s);
	public NewSignalFoundDelegate NewSignalFoundEvent;

	private Signal lastSignalReceived;

	protected void Start (){
		GameObject managerObject;
		#if UNITY_EDITOR
		managerObject = GameObject.Instantiate(Resources.Load("Desktop_Bluetooth_Manager")) as GameObject;
		#else
		managerObject = GameObject.Instantiate(Resources.Load("Mobile_Bluetooth_Manager")) as GameObject;
		#endif
		managerObject.transform.SetParent(transform);
		Manager = managerObject.GetComponent<BluetoothManager> ();

		lastSignalReceived = SignalUtils.NullSignal;

		Manager.SignalReceivedEvent += ManagerReceivedSignal;
	}

	private void ManagerReceivedSignal(Signal s){
		if (s.GetSignature() != lastSignalReceived.GetSignature () || s.GetPayload () != lastSignalReceived.GetPayload ()) {
			if (NewSignalFoundEvent != null) {
				NewSignalFoundEvent (s);
			}
		}
	}


}