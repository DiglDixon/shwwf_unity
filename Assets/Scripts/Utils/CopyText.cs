using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent (typeof(Text))]
public class CopyText : MonoBehaviour {

	public Text text;
	private Text thisText;

	private void Start(){
		thisText = GetComponent<Text> ();
	}

	private void Update () {
		thisText.text = text.text;
	}
}
