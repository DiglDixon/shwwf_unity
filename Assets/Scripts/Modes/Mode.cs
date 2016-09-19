using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Mode : MonoBehaviour{

	private PasswordCheck[] validPasswords;

	private void Awake(){
		validPasswords = GetComponentsInChildren<PasswordCheck> ();
	}

	public bool PasswordIsCorrect(string password) {
		for(int k = 0; k<validPasswords.Length; k++) {
			if(validPasswords[k].IsValidPassword(password)){
				return true;
			}
		}
		return false;
	}

	public abstract ModeName ModeName {
		get;
	}

	public abstract void ModeSelected ();

	public abstract void ModeDeselected ();

	public abstract void BeginShow ();

	public abstract void NewSceneLoaded (Scene scene);

	protected void EnableObjectsWithTag(string tag){
		GameObject[] gs =  GameObject.FindGameObjectsWithTag (tag);
		for (int k = 0; k < gs.Length; k++) {
			gs [k].SetActive (true);
		}
	}

}