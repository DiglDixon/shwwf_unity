
public class SkipSetupCommand : DebugCommand {

	public string skipAsAudiencePassword;
	public string skipAsGuidePassword;

	public override bool isValidCommand (string c){
		bool ret = (c.Equals (skipAsAudiencePassword) || c.Equals (skipAsGuidePassword));
		// this is super hacky..
		if (!ret) {
			ShowMode.Instance.SetSkippingSetup (false);
		}
		return ret;
	}

	public override void RunCommand (string c){
		ShowMode.Instance.SetSkippingSetup (true);
		if(c.Equals(skipAsAudiencePassword)){
			ShowMode.Instance.SetMode (ModeName.AUDIENCE);
		}else{
			ShowMode.Instance.SetMode (ModeName.GUIDE);
		}
	}

}