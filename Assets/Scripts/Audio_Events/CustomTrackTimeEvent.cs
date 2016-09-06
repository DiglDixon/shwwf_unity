﻿using UnityEngine;

public abstract class CustomTrackTimeEvent : MonoBehaviour{
	/// <summary>
	/// Means this will occur even if skipped over.
	/// </summary>
	public bool isStateEvent = false;
	private float minimumOccurAtTime = 0.1f;
	public float occurAtTime = 0.1f;
	public bool occurAtTimeFromEnd = false;

	#if UNITY_EDITOR
	public bool updateName = false;
	#endif

	public abstract void CustomEvent();

	#if UNITY_EDITOR
	private void OnValidate(){
		updateName = false;
		gameObject.name = GetObjectName();
		occurAtTime = Mathf.Max (minimumOccurAtTime, occurAtTime);
	}

	protected virtual string GetObjectName(){
		return this.GetType ().Name;
	}
	#endif

}
