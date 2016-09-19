
public class SetRoughTimeSyncAction : PayloadEventAction{

	public TimeSyncSetter syncSetter;

	public override void FireEvent (Signal s){
		syncSetter.SetRoughSyncFromSignal (s);
	}

}