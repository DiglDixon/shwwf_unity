using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Animator))]
public class DesktopAnimator : MonoBehaviour{

//	private LerpFloat animationLerp = new LerpFloat();
	private Animator animator;
	private RectTransform rectTransform;

	private void Awake(){
		animator = GetComponent<Animator> ();
		rectTransform = GetComponent<RectTransform> ();
	}

	public void SetPivot(Vector2 pivot){
		rectTransform.pivot = pivot;
	}

	public void ZoomOut(){
		animator.SetBool ("zoomed_out", true);
	}

	public void ZoomIn(){
		animator.SetBool ("zoomed_out", false);
	}
}