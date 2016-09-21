using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonPresser : MonoBehaviour{

	PointerEventData pointer = new PointerEventData(EventSystem.current);

	public void PressButton(GameObject b, float t){
		StartCoroutine (PressButtonRoutine (b, t));
	}

	private IEnumerator PressButtonRoutine(GameObject go, float time){
		ExecuteEvents.Execute(go, pointer, ExecuteEvents.pointerEnterHandler);
		ExecuteEvents.Execute(go, pointer, ExecuteEvents.pointerDownHandler);
		yield return new WaitForSeconds(time);
		ExecuteEvents.Execute(go, pointer, ExecuteEvents.submitHandler);
		ExecuteEvents.Execute(go, pointer, ExecuteEvents.pointerUpHandler);
		ExecuteEvents.Execute(go, pointer, ExecuteEvents.pointerExitHandler);
		Debug.Log ("End fake button press on "+go.name);
	}
}