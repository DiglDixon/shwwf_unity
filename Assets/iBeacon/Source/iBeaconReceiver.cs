using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

[ExecuteInEditMode]
[RequireComponent(typeof(BluetoothState))]
public class iBeaconReceiver : MonoBehaviour {
#if iBeaconDummy
	[System.Serializable]
	public class DummyRegion {
		public string regionName;
		public string UUID;
	}
	public DummyRegion[] regions;
	public string NSLocationUsageDescription;
#endif

	public delegate void BeaconRangeChanged(Beacon[] beacons);

	public static event BeaconRangeChanged BeaconRangeChangedEvent;

	[Obsolete("BluetoothStateChangedEvent is deprecated, please use BluetoothState.BluetoothStateChangedEvent instead.")]
	public static event BluetoothState.BluetoothStateChanged BluetoothStateChangedEvent {
		add {
			BluetoothState.BluetoothStateChangedEvent += value;
		}
		remove {
			BluetoothState.BluetoothStateChangedEvent -= value;
		}
	}

#if !iBeaconDummy
	public static iBeaconRegion[] regions {
		get {
			return m_instance._regions;
		}
		set {
			m_instance._regions = value;
			initialized = false;
		}
	}
#endif

	[SerializeField]
	private iBeaconRegion[] _regions;
#if UNITY_EDITOR
	[SerializeField]
	private string _NSLocationUsageDescription = "Why do you want to know the location?";
	[SerializeField]
	[HideInInspector]
	private string _OldNSLocationUsageDescription = "";

	#if !iBeaconDummy
	public static string NSLocationUsageDescription {
		get {
			return m_instance._NSLocationUsageDescription;
		}
		set {
			m_instance._NSLocationUsageDescription = value;
			m_instance.UpdateDescription();
		}
	}
	#endif
#endif

#if UNITY_ANDROID
	private static AndroidJavaObject plugin;
#endif

	private static iBeaconReceiver m_instance;

	private static bool initialized = false;

	private static bool receiving = false;

	private void Awake() {
#if iBeaconDummy
		Debug.LogError("iBeaconDummy is still on! Please remove it from the Scripting Define Symbols.");
#endif
		if (m_instance != null && m_instance != this) {
#if UNITY_EDITOR
			DestroyImmediate(this);
#else
			Destroy(this);
#endif
			return;
		}
#if UNITY_EDITOR
		if (!gameObject.name.Equals(BluetoothState.NAME)) {
			var obj = GameObject.Find(BluetoothState.NAME);
			if (obj == null) {
				gameObject.name = BluetoothState.NAME;
			} else {
	#if !iBeaconDummy
				var component = obj.AddComponent<iBeaconReceiver>();
				component._regions = _regions;
				DestroyImmediate(this);
				return;
	#endif
			}
		}
#endif
		m_instance = this;
		initialized = false;
		receiving = false;
#if UNITY_EDITOR
		UpdateDescription();
#endif
	}

	private void OnDestroy() {
		if (m_instance == this) {
			m_instance = null;
		}
	}

#if UNITY_IOS
	[DllImport("__Internal")]
	private static extern bool InitReceiver(string regions, bool shouldLog);

	[DllImport("__Internal")]
	private static extern void StartIOSScan();

	[DllImport("__Internal")]
	private static extern void StopIOSScan();
#endif

	public static void Restart() {
		Stop();
		Scan();
	}

	[Obsolete("Init() is deprecated, please remove the usage or use Restart() instead.")]
	public static void Init() {
		InternInit(true);
	}

	[Obsolete("Init(bool) is deprecated, please remove the usage or use Restart() instead.")]
	public static void Init(bool shouldLog) {
		InternInit(shouldLog);
	}

	private static void InternInit(bool shouldLog) {
		if (initialized) {
			Scan();
			return;
		}
		if (m_instance == null) {
			m_instance = FindObjectOfType<iBeaconReceiver>();
			if (m_instance == null) {
				BluetoothState.Init();
				m_instance = GameObject.Find(BluetoothState.NAME).AddComponent<iBeaconReceiver>();
			}
		}
		Stop();
		if (BluetoothState.GetBluetoothLEStatus() != BluetoothLowEnergyState.POWERED_ON) {
			BluetoothState.EnableBluetooth();
			if (BluetoothState.GetBluetoothLEStatus() != BluetoothLowEnergyState.POWERED_ON) {
				throw new iBeaconException("Bluetooth is off and could not be enabled.");
			}
		}
#if !UNITY_EDITOR
	#if UNITY_IOS
		if (!InitReceiver(iBeaconRegion.regionsToString(m_instance._regions), shouldLog)) {
			throw new iBeaconException("Receiver initialization failed.");
		}
	#elif UNITY_ANDROID
		GetPlugin().Call("Init", iBeaconRegion.regionsToString(m_instance._regions), shouldLog);
	#endif
#endif
		initialized = true;
		Scan();
	}

	public static void Stop() {
		if (!receiving) {
			return;
		}
#if !UNITY_EDITOR
	#if UNITY_IOS
		StopIOSScan();
	#elif UNITY_ANDROID
		GetPlugin().Call("Stop");
	#endif
#endif
		receiving = false;
	}

	public static void Scan() {
		if (!initialized) {
			InternInit(true);
			return;
		}
		if (receiving) {
			return;
		}
#if !UNITY_EDITOR
	#if UNITY_IOS
		StartIOSScan();
	#elif UNITY_ANDROID
		GetPlugin().Call("Scan");
	#endif
#endif
		receiving = true;
	}

	[Obsolete("EnableBluetooth() is deprecated, please use BluetoothState.EnableBluetooth() instead.")]
	public static void EnableBluetooth() {
		BluetoothState.EnableBluetooth();
	}

#if UNITY_ANDROID
	private static AndroidJavaObject GetPlugin() {
		if (plugin == null) {
			plugin = new AndroidJavaObject("com.kaasa.ibeacon.BeaconService");
		}
		return plugin;
	}
#endif

	private static T[] GetJsonArray<T>(string json) {
		string newJson = "{\"array\":" + json + "}";
		JsonWrapper<T> wrapper = JsonUtility.FromJson<JsonWrapper<T>>(newJson);
		return wrapper.array;
	}

	[Serializable]
	private class JsonWrapper<T> {
		public T[] array = null;
	}

	private void RangeBeacons(string beacons) {
		if (BeaconRangeChangedEvent != null) {
			Beacon[] beaconArray = GetJsonArray<Beacon>(beacons);
			foreach (var b in beaconArray) {
				b.ResetLastSeen();
			}
			BeaconRangeChangedEvent(beaconArray);
		}
	}

#if UNITY_EDITOR
	private void UpdateDescription() {
		if (!_NSLocationUsageDescription.Equals(_OldNSLocationUsageDescription)) {
			PlayerPrefs.SetString("NSLocationUsageDescription", _NSLocationUsageDescription);
			_OldNSLocationUsageDescription = _NSLocationUsageDescription;
		}
	}

	private void Update() {
		UpdateDescription();
	}
#endif
}
