﻿using UnityEngine;
using System.Collections;

public class LockScreenEvent : SingleRegionCustomEvent {
	
//	public bool locked = true;

//	protected override string GetNameDetails ()
//	{
//		return (locked?"LOCK_PHONE":"UNLOCK_PHONE");
//	}

	public override void CustomEvent (){
		AppAnimator.Instance.PhoneLocked ();
//		if (locked) {
//			AppAnimator.Instance.PhoneLocked ();
//		} else {
//			AppAnimator.Instance.PhoneUnlocked ();
//		}
	}

	public override void CustomExitRegionEvent (){
		AppAnimator.Instance.PhoneUnlocked ();
	}
}
