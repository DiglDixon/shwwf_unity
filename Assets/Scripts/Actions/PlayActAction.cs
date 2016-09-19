
public class PlayActAction : PayloadEventAction{

	public Act act;
	public TracklistPlayer player;

	protected override string GetGameObjectName (){
		string actName;
		if (act == null) {
			actName = "(undef)";
		} else {
			actName = act.definedAct.ToString();
		}
		return base.GetGameObjectName ()+":"+actName;
	}

	public override void FireEvent (Signal s){
		player.BeginActFromSignal (act, s);
	}

}