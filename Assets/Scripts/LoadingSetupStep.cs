using UnityEngine;

public class LoadingSetupStep : SetupStep{

	public AudioClip clipToLoad;

	public override void Activate (ShowSetup callback){
		base.Activate (callback);
		clipToLoad.LoadAudioData ();
	}

	protected override bool SetupCompleteCondition (){
		return (clipToLoad.loadState == AudioDataLoadState.Loaded);
	}

	protected override void ResetConditions (){
		// nothing required.
	}

}