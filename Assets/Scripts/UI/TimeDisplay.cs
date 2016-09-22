using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

[RequireComponent (typeof(Text))]
public class TimeDisplay : MonoBehaviour {

	private Text text;

	public bool includeAmPm = true;

	void Awake () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		System.DateTime time = System.DateTime.Now;
		StringBuilder sb = new StringBuilder ();
		sb.Append ((time.Hour > 12 ? time.Hour - 12 : time.Hour).ToString());
		sb.Append (":");
		sb.Append (time.Minute.ToString("00"));
		if (includeAmPm) {
			sb.Append (time.Hour > 11 ? "PM" : "AM");
		}
		text.text = sb.ToString ();
	}
}
