using UnityEngine;


public class SequenceWithBackground : DisplaySequence{

	public SequenceObject background;

	public override void Begin (){
		base.Begin ();
		background.BeginSequence ();
	}

	protected override void EntireSequenceFinished (){
		background.EndSequence();
	}

	protected override void OnDisable (){
		base.OnDisable ();
		background.SequenceExitedEvent -= BackgroundExitFinished;
	}

	protected override void OnEnable (){
		base.OnEnable ();
		background.SequenceExitedEvent += BackgroundExitFinished;
	}

	private void BackgroundExitFinished(){
		gameObject.SetActive(false);
	}

	public override void SkipSequence (){
		base.SkipSequence();
		background.CancelSequence();
	}

}