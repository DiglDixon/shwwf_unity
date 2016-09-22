using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Text))]
public class DateText : MonoBehaviour {

	private Text text;

	private void Awake(){
		FindAndModifyText ();
	}

	private void FindAndModifyText(){
		if (text == null) {
			text = GetComponent<Text> ();
			text.text = GetDateString ();
		}
	}

	private string GetDateString(){
		System.DateTime now = System.DateTime.Now;
		if (Variables.Instance.language == Language.ENGLISH) {
			return GetWeekDay()+", "+GetDay()+" "+GetMonth();
		} else {
			return GetMonth () + GetDay () + " " + GetWeekDay ();
		}
	}

	private string GetWeekDay(){
		if (Variables.Instance.language == Language.ENGLISH) {
			return System.DateTime.Now.DayOfWeek.ToString ();
		} else {
			switch (System.DateTime.Now.DayOfWeek.ToString ()) {
			case "Monday":
				return "星期一";
			case "Tuesday":
				return "星期二";
			case "Wednesday":
				return "星期三";
			case "Thursday":
				return "星期四";
			case "Friday":
				return "星期五";
			case "Saturday":
				return "星期六";
			case "Sunday":
				return "星期日";
			default:
				return "";
			}
		}
	}

	private string GetDay(){
		int m = System.DateTime.Now.Day;
		if (Variables.Instance.language == Language.ENGLISH) {
			return m.ToString ();
		} else {
			return m.ToString()+"月";
		}
	}

	private string GetMonth(){
		int m = System.DateTime.Now.Month;
		if (Variables.Instance.language == Language.ENGLISH) {
			switch (m) {
			case 9:
				return "September";
			default:
				return "October";
			}
		} else {
			return m.ToString()+"月";
		}
	}
}
