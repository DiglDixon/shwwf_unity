using UnityEngine;

// This is a mono so it doesn't get scooped by the Mode it's attached to.
public class DatePassword : MonoBehaviour{

	public int month;
	public int day;
	public string mandarinPassword;
	public string englishPassword;

	public bool updateName = false;

	public virtual void UpdateName(){
		gameObject.name = (HasPassword()? "DEF_" : "UNDEF_") + month + "-" + day + ": " + englishPassword + ", " + mandarinPassword;
	}

	protected virtual void OnValidate(){
		updateName = false;
		UpdateName ();
	}

	public bool IsValidPassword(string p){
		return p.Equals (mandarinPassword) || p.Equals (englishPassword);
	}

	public bool HasPassword(){
		return mandarinPassword != "" && englishPassword != "";
	}

}