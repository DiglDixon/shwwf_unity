using UnityEngine;

public abstract class CustomTrackTimeEvent : MonoBehaviour{
	/// <summary>
	/// Means this will occur even if skipped over.
	/// </summary>
	public bool isStateEvent = true;
	private float minimumOccurAtTime = 0.1f;
	public float occurAtTime = 0.1f;
	public bool occurAtTimeFromEnd = false;

	public bool updateName = false;

	public abstract void CustomEvent();

	protected virtual void OnValidate(){
		updateName = false;
		gameObject.name = GetObjectName();
		occurAtTime = Mathf.Max (minimumOccurAtTime, occurAtTime);
	}

	protected virtual string GetObjectName(){
		return GetObjectPrefix()+GetNameDetails()+GetTimeAtString();
	}

	protected virtual string GetObjectPrefix(){
		return (isStateEvent ? "S_" : "M_");
	}

	protected virtual string GetNameDetails(){
		return this.GetType ().Name;
	}

	protected virtual string GetTimeAtString(){
		return "@" + (occurAtTimeFromEnd ? "-" : "+") + occurAtTime;
	}

}
