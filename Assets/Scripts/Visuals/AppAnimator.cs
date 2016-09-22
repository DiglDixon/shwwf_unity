using UnityEngine;
using System.Collections;

public class AppAnimator : Singleton<AppAnimator>{

	public DesktopAnimator desktopAnimator;

	public FakeApp toOpenOne;
	public FakeApp toOpenTwo;

	public AppDisplayElement lockScreen;
	public AppDisplayElement lockScreenDark;

	private void Start(){
		lockScreen.gameObject.SetActive (true);
	}

	private void Update(){
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			PhoneLocked ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			PhoneAwoken ();
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			PhoneUnlocked ();
		}
	}

	public void PhoneLocked(){
		StartCoroutine (RunPhoneLockedRoutine());
	}

	public void PhoneAwoken(){
		lockScreenDark.Exit ();
	}

	public void PhoneUnlocked(){
		lockScreen.Exit ();
		desktopAnimator.ZoomIn ();
	}

	private IEnumerator RunPhoneUnlockedRoutine(){
		lockScreenDark.Enter();
		yield return new WaitForSeconds (0.3f);
		lockScreen.Enter ();
		desktopAnimator.ZoomOut ();
	}

	private IEnumerator RunPhoneLockedRoutine(){
		lockScreenDark.Enter();
		yield return new WaitForSeconds (0.3f);
		lockScreen.Enter ();
		desktopAnimator.ZoomOut ();
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