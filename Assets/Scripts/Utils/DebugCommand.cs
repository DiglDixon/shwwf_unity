using UnityEngine;

public abstract class DebugCommand : MonoBehaviour{

	public abstract bool isValidCommand(string c);

	public abstract void RunCommand(string c);

	private void OnValidate(){
		gameObject.name = "CMD: " + this.GetType ().Name;
	}

}