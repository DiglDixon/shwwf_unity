
public class TimeSyncAction : PayloadEventAction{

	public TimeSyncSetter setter;

	public override void FireEvent (Signal s){
		setter.SetRoughSyncFromSignal (s);
	}

}