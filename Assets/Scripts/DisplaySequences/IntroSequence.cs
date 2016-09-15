using UnityEngine;
using System.Collections;


public class IntroSequence : SequenceWithBackground{

	public GameObject disableOnBackgroundEnter;

	protected override void OnEnable ()
	{
		base.OnEnable ();
		background.SequenceEnteredEvent += BackgroundEntered;
	}

	protected override void OnDisable ()
	{
		base.OnEnable ();
		background.SequenceEnteredEvent -= BackgroundEntered;
	}

	private void BackgroundEntered(){
		disableOnBackgroundEnter.SetActive (false);
	}

	public override void SkipSequence (){
		base.SkipSequence ();
		BackgroundEntered ();
	}



}
