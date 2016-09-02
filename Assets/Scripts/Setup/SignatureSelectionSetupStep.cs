

public class SignatureSelectionSetupStep : SetupStep{

	protected override bool SetupCompleteCondition (){
		return (ShowMode.Instance.Signature != Signature.NONE);
	}

	protected override void ResetConditions (){
		// nothing required.
	}
}