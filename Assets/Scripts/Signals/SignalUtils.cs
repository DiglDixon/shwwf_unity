using System;
using UnityEngine;

public static class SignalUtils{
	
	public static Signal NullSignal{
		get{
			return new Signal (Signature.NONE, Payload.NONE);
		}
	}

	public static Signature[] GetAllSignatures (){
		Signature[] ret = new Signature[Enum.GetValues(typeof(Signature)).Length];
		for(int k = 0; k<ret.Length; k++){
			ret[k] = (Signature)k;
		}
		return ret;
	}

	private static readonly string[] uuids = {
		"5ff99f07-d609-4553-9dfb-37028ebd49bc"
	};

	public static string GetSignaureUUID(Signature signature){
		return uuids[ ( (int)signature % uuids.Length) ];
	}

	public static int GetSignalTimeOffset(SignalTime s){


		//New method. Presumes gap is less that 30 minutes. 

		DateTime ours = Variables.Instance.GetCurrentTimeWithOffset ();

		int ourMinute = ours.Minute;
		int ourSecond = ours.Second;
		int theirMinute = s.minute;
		int theirSecond = s.second;


//		Debug.Log ("Testing GetSignalOffsetTime: " + ourMinute + ", " + ourSecond + ", " + theirMinute + ", " + theirSecond);

		if (ourMinute == theirMinute) {
			// Happy days, we'll just grab the difference. Limit to min 0 - we can't skip backwards. Yet...
			return Mathf.Max(ourSecond-theirSecond, 0);
		}
		// else, someone is ahead of the other.

		// Cheap abs
		int minuteDiff = (ourMinute > theirMinute ? ourMinute - theirMinute : theirMinute - ourMinute);

		if (minuteDiff > 30) {
			// Someone has ticked the hour.
			if (ourMinute < theirMinute) {
				// We are ahead, and have ticked the hour
				return GetTimeDifferenceBetweenDifferentHours(ourMinute, ourSecond, theirMinute, theirSecond);
			} else {
				// They are ahead, and have ticked the hour
				return 0;// We min at 0 for now. // GetTimeDifferenceBetweenDifferentHours(theirMinute, theirSecond, ourMinute, ourSecond);
			}
		} else {
			// We are within the same hour
			if (ourMinute > theirMinute) {
				// We are ahead
				return GetTimeDifferenceWithinSameHour(ourMinute, ourSecond, theirMinute, theirSecond);
			} else {
				// They are ahead
				return 0;// We min at 0 for now.
			}
		}

		// Old method below. Presumes gap less than 60 minutes. Presumes receiver has a larger time than sender.
		/*
		DateTime now = Variables.Instance.GetCurrentTimeWithOffset ();

		int timeSecond = now.Second;
		int timeMinute = now.Minute;

		int nowMinute = timeMinute;
		int prevMinute = s.minute;

		// If the current minute is smaller than the previous one, we must have ticked an hour
		if (nowMinute < prevMinute) {
			nowMinute += 60;
		} 

		int minuteDifference = nowMinute - prevMinute;
		if (minuteDifference == 0) {
			return timeSecond - s.second;
		} else {
			int ret = (60 - s.second) + timeSecond + 60 * (Mathf.Max (minuteDifference - 1, 0));
			return ret;
		}*/
	}

	private static int GetTimeDifferenceBetweenDifferentHours(int nextHourMinute, int nextHourSecond, int prevHourMinute, int prevHourSecond){
		TimeSpan aheadSpan = new TimeSpan (1, nextHourMinute, nextHourSecond);
		TimeSpan behindSpan = new TimeSpan (0, prevHourMinute, prevHourSecond);
		return (int)aheadSpan.Subtract (behindSpan).TotalSeconds; // I think rounding is redundant. The figure will be clean.
	}

	private static int GetTimeDifferenceWithinSameHour(int aheadMinute, int aheadSecond, int behindMinute, int behindSecond){
		TimeSpan aheadSpan = new TimeSpan (0, aheadMinute, aheadSecond);
		TimeSpan behindSpan = new TimeSpan (0, behindMinute, behindSecond);
		return (int)aheadSpan.Subtract (behindSpan).TotalSeconds;
	}

	public static SignalTime GetSignalTime(){
		return new SignalTime (
			System.DateTime.Now.Minute, // These do not pull from the offsets. I think this is ok - prevents guides having offsets
			System.DateTime.Now.Second
		);
	}

}