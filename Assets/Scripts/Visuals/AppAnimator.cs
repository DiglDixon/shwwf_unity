using UnityEngine;


public class AppAnimator : MonoBehaviour{

	public DesktopAnimator desktopAnimator;

	public FakeApp toOpenOne;
	public FakeApp toOpenTwo;

	private void Update(){
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			OpenFakeApp (toOpenOne);
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			CloseFakeApp (toOpenOne);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			OpenFakeApp (toOpenTwo);
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			CloseFakeApp (toOpenTwo);
		}
	}

	public void OpenFakeApp(FakeApp fa){
		desktopAnimator.SetPivot (fa.GetEntrancePivot ());
		desktopAnimator.ZoomOut();
		fa.Enter();
	}

	public void CloseFakeApp(FakeApp fa){
		desktopAnimator.SetPivot (fa.GetEntrancePivot ());
		desktopAnimator.ZoomIn();
		fa.Exit();
	}

}