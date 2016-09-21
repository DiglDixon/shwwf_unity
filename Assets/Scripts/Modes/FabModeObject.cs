using UnityEngine;

public class FabModeObject : MonoBehaviour{

	private void Awake(){
		if (!ShowMode.Instance.IsFabMode ()) {
			gameObject.SetActive (false);
		}
	}

}
