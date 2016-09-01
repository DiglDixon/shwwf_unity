using System.Collections.Generic;

public class FloatEventExtension{

	public delegate void FloatDelegate(float timeRemaining);
	public event FloatDelegate FloatEvent;
	private List<FloatDelegate> oneShotDelegates = new List<FloatDelegate> ();

	public void AddPermanentDelegate(FloatDelegate function){
		FloatEvent += function;
	}

	public void RemovePermanentDelegate(FloatDelegate function){
		FloatEvent -= function;
	}

	public void AddOneShotDelegate(FloatDelegate function){
		FloatEvent += function;
		oneShotDelegates.Add (function);
		Diglbug.Log ("Added oneShotDelegate, total: "+oneShotDelegates.Count, PrintStream.DELEGATES);
	}

	public void RemoveOneShotDelegate(FloatDelegate function){
		if (oneShotDelegates.Contains (function)) {
			oneShotDelegates.Remove (function);
			FloatEvent -= function;
		} else {
			Diglbug.LogError ("Unexpected call to RemoveOneShotDelegate");
		}
		Diglbug.Log ("Removed oneShotDelegate, total: "+oneShotDelegates.Count, PrintStream.DELEGATES);
	}

	public void ClearOneShotDelegates(){
		for (int k = 0; k<oneShotDelegates.Count; k++) {
			FloatEvent -= oneShotDelegates [k];
		}
		oneShotDelegates.Clear ();
		Diglbug.Log ("Cleared oneShotDelegates", PrintStream.DELEGATES);
	}

	public void TriggerEvent(float v){
		if(FloatEvent!=null)
			FloatEvent (v);
		ClearOneShotDelegates ();
	}
}