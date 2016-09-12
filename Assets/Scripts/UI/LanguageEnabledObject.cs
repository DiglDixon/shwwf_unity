
public class LanguageEnabledObject : LanguageElement{

	public Language enabledOn;

	public override void SwitchToLanguage (Language l){
		if (enabledOn == l) {
			gameObject.SetActive (true);
		} else {
			gameObject.SetActive (false);
		}
	}

}