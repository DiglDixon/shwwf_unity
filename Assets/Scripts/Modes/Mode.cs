using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Mode : MonoBehaviour{

	public string[] validPasswords;

	public bool PasswordIsCorrect(string password) {
		string s;
		for(int k = 0; k<validPasswords.Length; k++) {
			s = validPasswords[k];
			if (s.Equals (password)) {
				return true;
			}
		}
		return false;
	}

	public abstract string ModeName {
		get;
	}

	public abstract void ModeSelected ();

	public abstract void ModeDeselected ();

	public abstract void BeginShow ();

	public abstract void NewSceneLoaded (Scene scene);

}