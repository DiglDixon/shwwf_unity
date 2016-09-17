using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Text))]
public class TimeDisplay : MonoBehaviour {

	private Text text;

	void Awake () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		System.DateTime time = System.DateTime.Now;

		text.text = (time.Hour > 12 ? time.Hour - 12 : time.Hour) + ":" + time.Minute + " " + (time.Hour > 11 ? "PM" : "AM");
	}
}
