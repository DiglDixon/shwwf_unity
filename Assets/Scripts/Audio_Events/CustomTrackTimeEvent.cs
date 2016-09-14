using UnityEngine;

public abstract class CustomTrackTimeEvent : MonoBehaviour{
	/// <summary>
	/// Means this will occur even if skipped over.
	/// </summary>
	public bool isStateEvent = false;
	private float minimumOccurAtTime = 0.1f;
	public float occurAtTime = 0.1f;
	public bool occurAtTimeFromEnd = false;

	public bool updateName = false;

	public abstract void CustomEvent();

	private void OnValidate(){
		updateName = false;
		gameObject.name = GetObjectName();
		occurAtTime = Mathf.Max (minimumOccurAtTime, occurAtTime);
	}

	protected virtual string GetObjectName(){
		return this.GetType ().Name;
	}

}
