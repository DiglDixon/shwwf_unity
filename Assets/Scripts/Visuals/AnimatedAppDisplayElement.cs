using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedAppDisplayElement : AppDisplayElement{

	private Animator animator;

	protected virtual void Awake(){
		animator = GetComponent<Animator>();
	}

	public override void Enter(){
		gameObject.SetActive (true);
		animator.SetBool ("open", true);
	}

	public override void Exit(){
		if (gameObject.activeSelf) {
			animator.SetBool ("open", false);
		} else {
			Diglbug.Log ("Ignored a close request for " + name + " because we were inactive", PrintStream.ANIMATION);
		}
	}

	public void AppFinishedOpening(){
		Diglbug.Log ("OPENED "+name);
	}

	public void AppFinishedClosing(){
		gameObject.SetActive (false);
		Diglbug.Log ("CLOSED "+name);
	}


}