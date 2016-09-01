using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ListDisplayItem : MonoBehaviour {
	
	private int index;
	private ListDisplay listDisplayParent;

	public void SetListDisplayParent(ListDisplay p){
		this.listDisplayParent = p;
	}

	public void SetIndex(int i){
		this.index = i;
	}

	public void ButtonPressed(){
		listDisplayParent.ItemPressed (index);
	}

}
