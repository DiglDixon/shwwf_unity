

public abstract class AppControllerEvent : CustomTrackTimeEvent{

	public AppController appController;

	protected override void OnValidate ()
	{
		base.OnValidate ();
		if (appController == null) {
			appController = FindObjectOfType<AppController> () as AppController;
		}
	}

}
