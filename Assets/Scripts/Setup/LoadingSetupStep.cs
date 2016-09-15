using UnityEngine;

public class LoadingSetupStep : SetupStep{

	public TracklistPlayer player;
	public QuietStartLoadSetupStep quietLoadStep;

	public override void Activate (ShowSetup callback){
		base.Activate (callback);
		// loaded in the quiet step now
	}

	protected override bool SetupCompleteCondition (){
		return quietLoadStep.entryTrack.GetTrack().IsLoaded();
	}

	protected override void ResetConditions (){
		// nothing required.
	}

}