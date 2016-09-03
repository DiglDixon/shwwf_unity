
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

	public Signal (Signature s, Payload p){
		SetSignature (s);
		SetPayload (p);
	}
	// See description for the -1 explanation.
	public Signal (Beacon baseBeacon) : this ((Signature)(baseBeacon.major-1), (Payload)(baseBeacon.minor-1)){
	}

	private void SetSignature(Signature s){
		this.signature = s;
	}

	private void SetPayload(Payload p){
		this.payload = p;
	}

	public Beacon ToBeacon(){
		// See description for the +1 explanation.
		return new Beacon (SignalUtils.GetSignaureUUID (signature), (int)signature+1, (int)payload+1);
	}

	public Signature GetSignature(){
		return signature;
	}

	public Payload GetPayload(){
		return payload;
	}

	public string GetPrint(){
		return GetSignature () + ":" + GetPayload ();
	}
}

