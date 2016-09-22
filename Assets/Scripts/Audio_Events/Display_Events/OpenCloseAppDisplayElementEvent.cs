
public class OpenCloseAppDisplayElementEvent : SingleRegionCustomEvent{

	public AnimatedAppDisplayElement toOpenAndClose;

//	public bool isOpen = true;

	public override void CustomEvent (){
		toOpenAndClose.Enter ();
//		if (isOpen) {
//			toOpenAndClose.Enter ();
//		} else {
//			toOpenAndClose.Exit ();
//		}
	}
//
//	protected override void SetPair (OpenCloseAppDisplayElementEvent pair){
//		base.SetPair (pair);
//		pair.isOpen = !isOpen;
//		pair.regionPair = this;
//	}

	protected override string GetNameDetails ()
	{
		string t = toOpenAndClose == null? "undefined" : toOpenAndClose.name;
		return "OPEN/CLOSE_"+t;
	}

	public override void CustomExitRegionEvent (){
		toOpenAndClose.Exit ();
	}


}