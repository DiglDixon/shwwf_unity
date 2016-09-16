

public class Amap : AppControllerStopStartEvent {
	public override void FireBeginningEvent (){
		appController.AmapBegins ();
	}
	public override void FireEndEvent (){
		appController.AmapEnds ();
	}
}
