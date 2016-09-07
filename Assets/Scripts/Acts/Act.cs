using UnityEngine;
using System.Collections;

public class Act : EnsurePayloadChild{

	public string ActName;
	private EventTracklistEntry[] actEntries = new EventTracklistEntry[0];
	private float totalTrackLength;
	private float inverse_totalTrackLength;
	private float[] normalisedTrackLengths;

	public TracklistPlayer player;

	public Payload entryPayload;

	private void Start(){
		
		actEntries = new EventTracklistEntry[transform.childCount];
		for (int k = 0; k < transform.childCount; k++) {
			actEntries [k] = transform.GetChild (k).GetComponent<EventTracklistEntry> ();
		}
//		Diglbug.Log ("Act Entries for " + name + ": " + actEntries.Length);
		totalTrackLength = 0f;
		for (int k = 0; k < actEntries.Length; k++) {
//			Diglbug.Log ("Iterating " + actEntries [k].name);
//			Diglbug.Log ("Iterating " + actEntries [k].GetTrack().GetTrackName());
			totalTrackLength += actEntries [k].GetTrackLength ();
		}
		inverse_totalTrackLength = 1f / totalTrackLength;

		normalisedTrackLengths = new float[actEntries.Length];
		for (int k = 0; k < normalisedTrackLengths.Length; k++) {
			normalisedTrackLengths [k] = actEntries [k].GetTrackLength () * inverse_totalTrackLength;
		}
	}

	public float GetProgress(){
		ITrack currentTrack = player.GetTrack ();
		for (int k = 0; k < actEntries.Length; k++) {
			if(currentTrack == actEntries[k].GetTrack()){
				return GetProgressFromEntryIndex (k);
			}
		}
		Diglbug.Log ("Warning: Called GetProgress from an Act that did not have a playing track", PrintStream.ACTS);
		return 0;
	}

	private float GetProgressFromEntryIndex(int i){
		float c = 0f;
		for (int k = 0; k < i; k++) {
			c += normalisedTrackLengths [k];
		}
		c += player.GetProgress () * normalisedTrackLengths[i];
		return c;
	}

	public bool ContainsTrack(ITrack t){
		for (int k = 0; k < actEntries.Length; k++) {
			if (actEntries [k].GetTrack () == t) {
				return true;
			}
		}
		return false;
	}

//	public void BeginAct(){
//		player.PlayTrackEntry (actEntries [0]);
//	}

	// EnsurePayloadChild overrides

	public override void SetPayload(Payload p){
		entryPayload = p;
	}

	public override Payload GetPayload(){
		return entryPayload;
	}

	protected override string GetNameString (){
		return "Act_" + entryPayload.ToString ();
	}


}