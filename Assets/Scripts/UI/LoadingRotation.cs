using UnityEngine;
using System.Collections;

public class LoadingRotation : MonoBehaviour {

	public Vector3 rotationSpeed = new Vector3(0f, 0, 0.4f);
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles + rotationSpeed * Time.deltaTime);
	}
}
