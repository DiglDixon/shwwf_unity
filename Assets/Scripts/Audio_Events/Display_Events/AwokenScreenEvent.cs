
public class AwokenScreenEvent : CustomTrackTimeEvent{

	public override void CustomEvent (){
		AppAnimator.Instance.PhoneAwoken ();
	}

}