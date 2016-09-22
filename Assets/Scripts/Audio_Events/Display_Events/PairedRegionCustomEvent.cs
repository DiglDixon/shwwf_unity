

public abstract class PairedRegionCustomEvent<PairType> : P_CustomRegionTrackTimeEvent where PairType : P_CustomRegionTrackTimeEvent{


	// Make friends with our child
	private void Awake(){
		regionPair = GetComponentInChildren<PairType> ();
		regionPair.regionPair = this;
	}

	public override float GetRegionEndTime (){
		if (regionPair == null) {
			return 0f;
		}
		return regionPair.occurAtTime; // this won't work for end times in this format.
	}

	protected virtual void SetPair(PairType pair){
		regionPair = pair;
	}

	protected override string GetObjectName ()
	{
		bool hasPair = false;
		if (regionPair == null) {
			PairType pair = null;
			for (int k = 0; k < transform.childCount; k++) {
				pair = transform.GetChild (k).gameObject.GetComponentInChildren<PairType> ();
				if (pair != null)
					break;
			}
			hasPair = (pair != null);
			if (hasPair) {
				SetPair (pair);
			}
		} else {
			hasPair = true;
		}

		return (hasPair?"PAIRED_":"UNPAIRED_")+base.GetObjectName ();
	}
}