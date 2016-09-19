using UnityEngine;
using System;
using System.Collections.Generic;


public class DatedPasswordSet : PasswordCheck{

	public int beginDate = 10;
	public int endDate = 31;

	public bool update = false;

	private DatePassword[] passwords;

	private void Awake(){
		passwords = GetComponentsInChildren<DatePassword> ();
	}

	private void OnValidate(){
		update = false;
		DatePassword[] existingChildren = GetComponentsInChildren<DatePassword> ();
		DatePassword existing;
		for (int k = beginDate; k <= endDate; k++) {
			existing = GetExistingDatePassword(existingChildren, k);
			if (existing == null) {
				GameObject newObject = new GameObject ();
				DatePassword newDatePassword = newObject.AddComponent<DatePassword> ();
				newDatePassword.month = 10;
				newDatePassword.day = k;
				newDatePassword.transform.SetParent (transform, false);
			}
		}

		existingChildren = GetComponentsInChildren<DatePassword> ();
		for (int k = 0; k < existingChildren.Length; k++) {
			existing = GetExistingDatePassword (existingChildren, beginDate+k);
			existing.transform.SetSiblingIndex (k);
			existing.UpdateName ();
		}

		gameObject.name = "DatePasswordSet ("+existingChildren.Length+")";
	}

	public override bool IsValidPassword(string password){
		int month = DateTime.Now.Month;
		int day = DateTime.Now.Day;
		Diglbug.Log ("Checking passwords with month and day: " + month + ", " + day);
		for (int k = 0; k < passwords.Length; k++) {
			if (passwords [k].month == month && passwords [k].day == day) {
				return passwords[k].IsValidPassword(password);
			}
		}
		return false;
	}

	private DatePassword GetExistingDatePassword(DatePassword[] children, int day){
		for (int k = 0; k < children.Length; k++) {
			if (children [k].day == day)
				return children [k];
		}
		return null;
	}
}
