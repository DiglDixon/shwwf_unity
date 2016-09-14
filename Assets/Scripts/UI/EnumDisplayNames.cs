
public static class EnumDisplayNames{
	
	public static string LanuageName(Language l){
		if (Variables.Instance.language == Language.ENGLISH) {
			return EnumDisplayNamesEnglish.LanuageName (l);
		} else {
			return EnumDisplayNamesMandarin.LanuageName (l);
		}
	}

	public static string PayloadName(Payload p){
		if (Variables.Instance.language == Language.ENGLISH) {
			return EnumDisplayNamesEnglish.PayloadName (p);
		} else {
			return EnumDisplayNamesMandarin.PayloadName (p);
		}
	}

	public static string SignatureName(Signature s){
		if (Variables.Instance.language == Language.ENGLISH) {
			return EnumDisplayNamesEnglish.SignatureName (s);
		} else {
			return EnumDisplayNamesMandarin.SignatureName (s);
		}
	}

	public static string DefinedActName(Signature s){
		if (Variables.Instance.language == Language.ENGLISH) {
			return EnumDisplayNamesEnglish.SignatureName (s);
		} else {
			return EnumDisplayNamesMandarin.SignatureName (s);
		}
	}

	public static string ActorName(Actor a){
		if (Variables.Instance.language == Language.ENGLISH) {
			return EnumDisplayNamesEnglish.ActorName (a);
		} else {
			return EnumDisplayNamesMandarin.ActorName (a);
		}
	}
}

public static class EnumDisplayNamesEnglish{

	public static string PayloadName(Payload p){
		switch (p) {
		default:
			Diglbug.LogWarning ("Failed to return a pre-defined PayloadName for " + p + " - please define one!");
			return "undefined_payload_string";
		}
	}

	public static string LanuageName(Language l){
		switch (l) {
		case Language.ENGLISH:
			return "English";
		case Language.MANDARIN:
			return "Mandarin";
		default:
			Diglbug.LogError ("Failed to return a pre-defined LanguageName for " + l + " - please define one!");
			return "undefined_language_string";
		}
	}

	public static string SignatureName(Signature s){
		switch (s) {
		case Signature.BLUE:
			return "Blue";
		case Signature.GREEN:
			return "Green";
		case Signature.NONE:
			return "None";
		case Signature.ORANGE:
			return "Orange";
		case Signature.PURPLE:
			return "Purple";
		case Signature.RED:
			return "Red";
		case Signature.YELLOW:
			return "Yellow";
		default:
			Diglbug.LogError ("Failed to return a pre-defined SignatureName for " + s + " - please define one!");
			return "undefined_signature_string";
		}
	}

	public static string DefinedActName(DefinedAct da){
		switch (da) {
		case DefinedAct.ACT_304:
			return "Room 304";
		case DefinedAct.ACT_ARRIVE_AT_SCHOOL:
			return "Arrive At STA";
		case DefinedAct.ACT_BAI_ROOM:
			return "Bai's Room";
		case DefinedAct.ACT_CROSSING_TO_SCHOOL:
			return "Crossing to STA";
		case DefinedAct.ACT_DORM:
			return "Dorm";
		case DefinedAct.ACT_DUANJUN:
			return "Duanjun";
		case DefinedAct.ACT_ELEVATOR:
			return "Elevator";
		case DefinedAct.ACT_GUANXINTAI:
			return "Guanxintai";
		case DefinedAct.ACT_INSIDE_DOOR:
			return "Kidnapping";
		case DefinedAct.ACT_RED_BUILDING:
			return "Red Building";
		case DefinedAct.ACT_ROOFTOP:
			return "Rooftop";
		case DefinedAct.ACT_SHOW_START:
			return "Opening Scene";
		case DefinedAct.ACT_YU_PINGFAN:
			return "Stairwell";
		default:
			Diglbug.LogError ("Failed to return a pre-defined DefinedActName for " + da + " - please define one!");
			return "undefined_signature_string";
		}
	}

	public static string ActorName(Actor a){
		switch (a) {
		case Actor.COUNSELLOR:
			return "Counsellor";
		case Actor.COUNSELLOR_ROOF:
			return "Counsellor (end)";
		case Actor.DORM_PERSON:
			return "Doorman";
		case Actor.PRODUCER:
			return "Producer";
		case Actor.STUDENT:
			return "Student";
		case Actor.VOLUNTEER:
			return "Volunteer";
		case Actor.YU_PINGFAN:
			return "Yu Pingfan";
		default:
			Diglbug.LogError ("Failed to return a pre-defined ActorName for " + a + " - please define one!");
			return "undefined_signature_string";
		}
	}


}

public static class EnumDisplayNamesMandarin{

	public static string PayloadName(Payload p){
		switch (p) {
		default:
			Diglbug.LogError ("Failed to return a pre-defined PayloadName for " + p + " - please define one!");
			return "undefined_payload_string";
		}
	}

	public static string LanuageName(Language l){
		switch (l) {
		case Language.ENGLISH:
			return "English";
		case Language.MANDARIN:
			return "Mandarin";
		default:
			Diglbug.LogError ("Failed to return a pre-defined LanguageName for " + l + " - please define one!");
			return "undefined_language_string";
		}
	}

	public static string SignatureName(Signature s){
		switch (s) {
		case Signature.BLUE:
			return "蓝";
		case Signature.GREEN:
			return "绿";
		case Signature.NONE:
			return "无";
		case Signature.ORANGE:
			return "橙";
		case Signature.PURPLE:
			return "紫";
		case Signature.RED:
			return "红";
		case Signature.YELLOW:
			return "黄";
		default:
			Diglbug.LogError ("Failed to return a pre-defined SignatureName for " + s + " - please define one!");
			return "undefined_signature_string";
		}
	}

	public static string DefinedActName(DefinedAct da){
		switch (da) {
		case DefinedAct.ACT_304:
			return "304房间";
		case DefinedAct.ACT_ARRIVE_AT_SCHOOL:
			return "到达上戏";
		case DefinedAct.ACT_BAI_ROOM:
			return "韩欣白的房间";
		case DefinedAct.ACT_CROSSING_TO_SCHOOL:
			return "穿过上戏";
		case DefinedAct.ACT_DORM:
			return "宿舍";
		case DefinedAct.ACT_DUANJUN:
			return "端钧剧场";
		case DefinedAct.ACT_ELEVATOR:
			return "电梯";
		case DefinedAct.ACT_GUANXINTAI:
			return "观心台";
		case DefinedAct.ACT_INSIDE_DOOR:
			return "绑架";
		case DefinedAct.ACT_RED_BUILDING:
			return "红楼";
		case DefinedAct.ACT_ROOFTOP:
			return "露台";
		case DefinedAct.ACT_SHOW_START:
			return "第一幕";
		case DefinedAct.ACT_YU_PINGFAN:
			return "楼梯间";
		default:
			Diglbug.LogError ("Failed to return a pre-defined DefinedActName for " + da + " - please define one!");
			return "undefined_signature_string";
		}
	}

	public static string ActorName(Actor a){
		switch (a) {
		case Actor.COUNSELLOR:
			return "辅导员";
		case Actor.COUNSELLOR_ROOF:
			return "辅导员 (结尾)";
		case Actor.DORM_PERSON:
			return "宿管";
		case Actor.PRODUCER:
			return "制作人";
		case Actor.STUDENT:// Boy student name: 男生
			return "女学生";
		case Actor.VOLUNTEER:
			return "志愿者";
		case Actor.YU_PINGFAN:
			return "余平凡";
		default:
			Diglbug.LogError ("Failed to return a pre-defined ActorName for " + a + " - please define one!");
			return "undefined_signature_string";
		}
	}


}