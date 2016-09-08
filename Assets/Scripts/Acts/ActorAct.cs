

public class ActorAct : Act{
	// uhm.

	protected override string GetNameString(){
		return base.GetNameString() + " @ "+entryPayload;
	}

}