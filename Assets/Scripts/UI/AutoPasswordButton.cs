using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Button))]
public class AutoPasswordButton : MonoBehaviour {

	public Text autoPassword;
	public PasswordControls passwordControls;

	public void ButtonPressed(){
		passwordControls.PasswordAttempted (autoPassword.text);
	}

}
