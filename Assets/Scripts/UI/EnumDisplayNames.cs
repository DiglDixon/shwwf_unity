
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
			Diglbug.LogError ("Failed to return a pre-defined SignatureName for " + da + " - please define one!");
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
			return "None";
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
			return "<Room 304>";
		case DefinedAct.ACT_ARRIVE_AT_SCHOOL:
			return "<Arrive At STA>";
		case DefinedAct.ACT_BAI_ROOM:
			return "<Bai's Room>";
		case DefinedAct.ACT_CROSSING_TO_SCHOOL:
			return "<Crossing to STA>";
		case DefinedAct.ACT_DORM:
			return "<Dorm>";
		case DefinedAct.ACT_DUANJUN:
			return "<Duanjun>";
		case DefinedAct.ACT_ELEVATOR:
			return "<Elevator>";
		case DefinedAct.ACT_GUANXINTAI:
			return "<Guanxintai>";
		case DefinedAct.ACT_INSIDE_DOOR:
			return "<Kidnapping>";
		case DefinedAct.ACT_RED_BUILDING:
			return "<Red Building>";
		case DefinedAct.ACT_ROOFTOP:
			return "<Rooftop>";
		case DefinedAct.ACT_SHOW_START:
			return "<Opening Scene>";
		case DefinedAct.ACT_YU_PINGFAN:
			return "<Stairwell>";
		default:
			Diglbug.LogError ("Failed to return a pre-defined SignatureName for " + da + " - please define one!");
			return "undefined_signature_string";
		}
	}


}