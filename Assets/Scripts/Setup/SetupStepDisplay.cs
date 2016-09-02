using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetupStepDisplay : MonoBehaviour {

	public Image incompleteImage;
	public Image completeImage;

	[TextArea]
	public string descriptionText;

	public string GetDescriptionText(){
		return descriptionText;
	}

	public void SetComplete(){
		completeImage.gameObject.SetActive (true);
		incompleteImage.gameObject.SetActive (false);
	}

	public void SetIncomplete(){
		completeImage.gameObject.SetActive (false);
		incompleteImage.gameObject.SetActive (true);
	}
}
