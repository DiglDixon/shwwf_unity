
public class OpenCloseAppDisplayElementEvent : PairedRegionCustomEvent<OpenCloseAppDisplayElementEvent>{

	public AnimatedAppDisplayElement toOpenAndClose;

	public bool isOpen = true;

	public override void CustomEvent (){
		if (isOpen) {
			toOpenAndClose.Enter ();
		} else {
			toOpenAndClose.Exit ();
		}
	}

	protected override void SetPair (OpenCloseAppDisplayElementEvent pair){
		base.SetPair (pair);
		pair.isOpen = !isOpen;
		pair.regionPair = this;
	}

	protected override string GetNameDetails ()
	{
		string t = toOpenAndClose == null? "undefined" : toOpenAndClose.name;
		return (isOpen?"OPEN_":"CLOSE_")+t;
	}


}