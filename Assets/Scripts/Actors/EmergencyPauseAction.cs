

public class EmergencyPauseAction : PayloadEventAction{

	public EmergencyPauseManager manager;

	public bool pause;

	protected override string GetGameObjectName ()
	{
		return (pause? "PAUSE_" : "UNPAUSE_")+base.GetGameObjectName ();
	}

	public override void FireEvent (Signal s){
		if (pause) {
			manager.EmergencyPauseSignalReceived ();
		} else {

			manager.EmergencyUnpauseSignalReceived ();
		}
	}

}