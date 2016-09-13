
public class HasUndefinedLanguage : LanguageElement{

	public override void SwitchToLanguage (Language l){
		Diglbug.LogWarning ("Warning! " + name + " is marked as having undefined language"); 
	}
}