using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(LerpableHost))]
public class SequenceObject : SequenceElement{

	public float entranceTime = 1f;
	public float duration = 2f;
	public float exitTime = 0.5f;
	public bool exitAfterDuration = false;

	private LerpFloat fader = new LerpFloat();

	public GameObject objectToEnable;
	public CanvasGroup canvasGroup;

	private void Start(){
		fader.SubscribeProcess (GetComponent<LerpableHost>());
	}

	private void OnEnable(){
		fader.LerpStepValueEvent += LerpStep;
		fader.LerpEndsValueEvents += LerpEnds;
	}

	private void OnDisable(){
		fader.LerpStepValueEvent -= LerpStep;
		fader.LerpEndsValueEvents -= LerpEnds;
	}

	public override void BeginSequence(){
		BeginEntrance ();
	}

	private void BeginEntrance(){
		objectToEnable.SetActive (true);
		fader.SetLerpFloatValue(0f);
		fader.LerpTo (1f, entranceTime);
	}

	private void LerpStep(float value){
		canvasGroup.alpha = value;
	}

	public void EndSequence(){
		BeginExit ();
	}

	private void BeginExit(){
		fader.LerpTo (0f, exitTime);
	}

	private void LerpEnds(float value){
		if (value == 1f) {
			EntranceComplete ();
		}
		if (value == 0f) {
			ExitComplete ();
		}
	}

	protected override void EntranceComplete (){
		base.EntranceComplete ();
		if (exitAfterDuration) {
			StartCoroutine (RunDurationCountdown ());
		}
	}

	protected override void ExitComplete(){
		base.ExitComplete ();
		objectToEnable.SetActive (false);
	}

	private IEnumerator RunDurationCountdown(){
		yield return new WaitForSeconds (duration);
		BeginExit ();
	}


}
