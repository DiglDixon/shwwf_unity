using UnityEngine;

public abstract class CustomTrackTimeEvent : MonoBehaviour{
	/// <summary>
	/// Means this will occur even if skipped over.
	/// </summary>
	public bool isStateEvent = false;
	public float occurAtTime = 0f;

	#if UNITY_EDITOR
	public bool updateName = false;
	#endif

	public abstract void CustomEvent();

	#if UNITY_EDITOR
	private void OnValidate(){
		updateName = false;
		gameObject.name = this.GetType ().Name;
	}
	#endif

}
