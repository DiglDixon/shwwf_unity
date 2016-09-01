using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// The base class for all Apps to extend.
/// 
/// </summary>

public abstract class AbstractApp{
	
	// Opens the app in a "generic" way (used, for example, when opening from tapping a notification)
	public abstract void Open();

	// Used for opening from things like buttons that the app appears to animate out of
	public abstract void OpenFromPosition (RectTransform fromPosition);

	// Force the app open immediately
	public abstract void OpenImmediate();

	// Closes the app in a "generic" way (used, for example, when hitting the Home button)
	public abstract void Close();

	// Used for opening from things like buttons that the app appears to animate out of
	public abstract void CloseToPosition (RectTransform toPosition);

	// Runs cleanups and closes the app immediately
	public abstract void CloseImmediate();

}

