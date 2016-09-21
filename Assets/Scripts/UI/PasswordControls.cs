using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PasswordControls : MonoBehaviour{

	public Text fieldPlaceholderText;
	public Color passwordFailedColour;

	public void PasswordAttempted(string password){
		if (password.Trim ().Length == 0)
			return;
		ShowMode.Instance.PasswordEntered (password);
		if (ShowMode.Instance.PasswordIsValid (password)) {
			//
		} else {
			PasswordAttemptFailed (password);
		}
	}

	private void PasswordAttemptFailed(string password){
		// do things
		Diglbug.Log ("Password attempt failed: "+password);
		StartCoroutine (RunPasswordFailed());
	}

	private IEnumerator RunPasswordFailed(){
		Color ogColour = fieldPlaceholderText.color;
		if(Variables.Instance.language == Language.ENGLISH){
			fieldPlaceholderText.text = "Password incorrect";
		}else{
			fieldPlaceholderText.text = "密码错误";
		}
		fieldPlaceholderText.color = passwordFailedColour;
		yield return new WaitForSeconds (2f);
		fieldPlaceholderText.GetComponent<LanguageToggleText> ().SwitchToLanguage (Variables.Instance.language);
		fieldPlaceholderText.color = ogColour;
	}

}
