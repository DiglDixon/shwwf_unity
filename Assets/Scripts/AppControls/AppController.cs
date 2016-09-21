using UnityEngine;

public class AppController : MonoBehaviour{





	public GameObject[] amapObjects;
	public GameObject[] phoneBaiObjects;
	public GameObject[] phoneCallFromMumObjects;
	public GameObject[] phoneCallToMumObjects;
	public GameObject[] sinaCompetitionObjects;
	public GameObject[] sinaKidnappedObjects;
	public GameObject[] tenCentKidnappedObjects;
	public GameObject[] textLandloadObjects;
	public GameObject[] weChatFromBaiObjects;
	public GameObject[] weChatVoiceFromBaiObjects;
	public GameObject[] weChatManagerObjects;
	public GameObject[] weChatVideoMumObjects;
	public GameObject[] yaozhiboObjects;

	public void AmapBegins(){
		RunOpen (amapObjects);
	}
	public void AmapEnds(){
		RunClose (amapObjects);
	}

	public void PhoneBaiBegins(){
		RunOpen (phoneBaiObjects);
	}
	public void PhoneBaiEnds(){
		RunClose (phoneBaiObjects);
	}

	public void PhoneFromMumBegins(){
		RunOpen (phoneCallFromMumObjects);
	}
	public void PhoneFromMumEnds(){
		RunClose (phoneCallFromMumObjects);
	}

	public void PhoneToMumBegins(){
		RunOpen (phoneCallToMumObjects);
	}
	public void PhoneToMumEnds(){
		RunClose (phoneCallToMumObjects);
	}

	public void SinaCompetitionBegins(){
		RunOpen (sinaCompetitionObjects);
	}
	public void SinaCompetitionEnds(){
		RunClose (sinaCompetitionObjects);
	}

	public void SinaKidnappedBegins(){
		RunOpen (sinaKidnappedObjects);
	}
	public void SinaKidnappedEnds(){
		RunClose (sinaKidnappedObjects);
	}

	public void TenCentKidnapBegins(){
		RunOpen (tenCentKidnappedObjects);
	}
	public void TenCentKidnapEnds(){
		RunClose (tenCentKidnappedObjects);
	}

	public void LandlordTextBegins(){
		RunOpen (textLandloadObjects);
	}
	public void LandlordTextEnds(){
		RunClose (textLandloadObjects);
	}

	public void WeChatFromBaiBegins(){
		RunOpen (weChatFromBaiObjects);
	}
	public void WeChatFromBaiEnds(){
		RunClose (weChatFromBaiObjects);
	}

	public void WeChatVoiceFromBaiBegins(){
		RunOpen (weChatVoiceFromBaiObjects);
	}
	public void WeChatVoiceFromBaiEnds(){
		RunClose (weChatVoiceFromBaiObjects);
	}

	public void WeChatManagerBegins(){
		RunOpen (weChatManagerObjects);
	}
	public void WeChatManagerEnds(){
		RunClose (weChatManagerObjects);
	}

	public void WeChatVideoMumBegins(){
		RunOpen (weChatVideoMumObjects);
	}
	public void WeChatVideoMumEnds(){
		RunClose (weChatVideoMumObjects);
	}

	public void YaozhiboBegins(){
		RunOpen (yaozhiboObjects);
	}
	public void YaozhiboEnds(){
		RunClose (yaozhiboObjects);
	}

	//

	private void RunOpen(GameObject[] gos){
		for(int k = 0; k<gos.Length; k++){
			RunOpen (gos[k]);
		}
	}

	private void RunClose(GameObject[] gos){
		for(int k = 0; k<gos.Length; k++){
			RunClose (gos[k]);
		}
	}

	private void RunOpen(GameObject go){
		go.SetActive (true);
	}

	private void RunClose(GameObject go){
		go.SetActive (false);
	}

}