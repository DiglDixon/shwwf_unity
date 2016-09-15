using UnityEngine;
/// <summary>
/// Our BLE Signal wrapper.
/// 
/// When translating to Beacons, we add 1 to minor and major values to avoid 0s.
/// I'd rather keep 0s as a valid option, otherwise there would be a lot of enum
/// catching going on.
/// 
/// When translating from Beacons, we subtract this 1 again.
/// 
/// </summary>
public class Signal{

	private Signature signature;
	private Payload payload;
	private SignalTime time;

	public Signal (Signature s, Payload p){
		SetSignature (s);
		SetPayload (p);
		time = SignalUtils.GetSignalTime ();
	}

	public Signal (Beacon baseBeacon){
		SetParametersFromBeacon (baseBeacon);
	}

	// ONLY USE THIS FOR RECOVERY - it has a pre-set time.
	public Signal (Signature s, Payload p, int minute, int second){
		SetSignature (s);
		SetPayload (p);
		time = new SignalTime(minute, second);
	}

	private void SetSignature(Signature s){
		this.signature = s;
	}

	private void SetPayload(Payload p){
		this.payload = p;
	}

	public Beacon ToBeacon(){
		// See description for the +1 explanation.
		int major = GetBeaconMajor() + 1;
		int minor = GetBeaconMinor () + 1;
		return new Beacon (SignalUtils.GetSignaureUUID (signature), major, minor);
	}
	/* Currently XSSPP where S is signature, P is payload. */
	private int GetBeaconMajor(){
		return (int)signature * 100 + (int)payload;
	}

	/* Currently XMMSS where M is minute, S is second. */
	private int GetBeaconMinor(){
		return time.minute * 100 + time.second;
	}

	private void SetParametersFromBeacon(Beacon b){
		int major = b.major - 1;
		int minor = b.minor - 1;

		int sig = Mathf.FloorToInt(major * 0.01f);
		int pay = major - sig * 100;

		int min = Mathf.FloorToInt (minor * 0.01f);
		int sec = minor - min * 100;

		Diglbug.Log ("Translating beacon into Signal: maj:" + b.major + ", min:" + b.minor + " == sig:" + sig + ", pay:" + pay+", minute:"+min+", second:"+sec);
		signature = (Signature)sig;
		payload = (Payload)pay;
		time = new SignalTime (min, sec);
	}

	public bool Equals(Signal s){
		return (s.signature == this.signature && s.payload == this.payload);
	}

	public Signature GetSignature(){
		return signature;
	}

	public Payload GetPayload(){
		return payload;
	}

	public SignalTime GetSignalTime(){
		return time;
	}

	public string GetPrint(){
		return GetSignature () + ":" + GetPayload ();
	}

	public string GetFullPrint(){
		return GetSignature () + ":" + GetPayload ()+"@"+time.minute+":"+time.second;
	}
}

