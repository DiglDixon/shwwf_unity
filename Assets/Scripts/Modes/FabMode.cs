
public class FabMode : AudienceMode{

	public override void ModeSelected (){
		base.ModeSelected ();
		ShowMode.Instance.SetFabMode (true);
	}

	public override void ModeDeselected (){
		base.ModeDeselected ();
		ShowMode.Instance.SetFabMode (false);
	}

}