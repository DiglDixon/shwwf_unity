using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetupStepDisplay : MonoBehaviour {

	public GameObject idleImage;
	public GameObject incompleteImage;
	public GameObject completeImage;

//	[TextArea]
//	public string descriptionText;
//
//	public string GetDescriptionText(){
//		return descriptionText;
//	}

	public void SetIdle(){
		idleImage.SetActive (true);
		completeImage.SetActive (false);
		incompleteImage.SetActive (false);
	}

	public void SetComplete(){
		completeImage.SetActive (true);
		idleImage.SetActive (false);
		incompleteImage.SetActive (false);
	}

	public void SetIncomplete(){
		completeImage.SetActive (false);
		idleImage.SetActive (false);
		incompleteImage.SetActive (true);
	}
}
