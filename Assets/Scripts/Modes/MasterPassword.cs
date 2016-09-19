using UnityEngine;

public class MasterPassword : PasswordCheck{

	public string passwordMandarin;
	public string passwordEnglish;

	public override bool IsValidPassword (string s){
		return s.Equals (passwordMandarin) || s.Equals (passwordEnglish);
	}

	public bool updateName = false;

	public virtual void UpdateName(){
		gameObject.name = "Master: " + passwordEnglish + ", " + passwordMandarin;
	}

	protected virtual void OnValidate(){
		updateName = false;
		UpdateName ();
	}

}
