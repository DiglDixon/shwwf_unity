using UnityEngine.UI;
public class PayloadEntryListDisplayItem : ListDisplayItem{

	public Text payloadText;

	public void SetPayload(Payload p){
		payloadText.text = p.ToString ();
	}

}