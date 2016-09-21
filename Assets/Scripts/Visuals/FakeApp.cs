using UnityEngine;
using UnityEngine.UI;

public class FakeApp : AnimatedAppDisplayElement{
	public RectTransform entranceObjectParent;
	public RectTransform entranceObject;

	private Vector2 pivot;
	private bool pivotAssigned = false;

	protected override void Awake(){
		base.Awake ();
		SetPivotFromEntranceObject ();
	}

	private void SetPivotFromEntranceObject(){
		pivot = new Vector2( entranceObject.anchoredPosition.x / entranceObjectParent.rect.size.x,
							1-(-entranceObject.anchoredPosition.y / entranceObjectParent.rect.size.y)
		);
		GetComponent<RectTransform> ().pivot = pivot;
		pivotAssigned = true;
	}

	public Vector2 GetEntrancePivot(){
		if (!pivotAssigned) {
			SetPivotFromEntranceObject ();
		}
		return pivot;
	}

}