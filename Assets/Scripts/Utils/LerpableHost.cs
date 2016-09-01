using UnityEngine;
using System.Collections.Generic;

public class LerpableHost : MonoBehaviour{

	public delegate void UpdateLerpDelegate ();
	public event UpdateLerpDelegate UpdateLerpEvent;

	protected virtual void Update(){
		UpdateLerps ();
	}

	protected void UpdateLerps(){
		if (UpdateLerpEvent != null)
			UpdateLerpEvent ();
	}
}

