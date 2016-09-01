using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ListDisplay : MonoBehaviour {

	public GameObject listParentTransform;
	private int itemCount = 0;

	public void AddListItem(ListEntry entry){
		GameObject newItem = entry.ConstructListObject ();
		newItem.transform.SetParent (listParentTransform.transform, false);
		ListDisplayItem itemScript = newItem.GetComponent<ListDisplayItem> ();
		itemScript.SetListDisplayParent (this);
		itemScript.SetIndex (itemCount);
		itemCount++;
	}

	public virtual void ItemPressed(int itemIndex){
		Diglbug.Log ("List " + name + " item pressed: " + itemIndex);
	}

	public void Open(){
		gameObject.SetActive (true);
	}

	public void Close(){
		gameObject.SetActive (false);
	}
}
