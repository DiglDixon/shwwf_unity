using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Text))]
public class FontSizeLerp : LerpableHost{

	public LerpFloat lerper = new LerpFloat();
	private Text text;

	private void Awake(){
		lerper.SubscribeProcess (this);
		text = GetComponent<Text> ();
	}

	private void OnEnable(){
		lerper.LerpStepValueEvent += LerpStepValue;
	}

	private void OnDisable(){
		lerper.LerpStepValueEvent -= LerpStepValue;
	}

	private void LerpStepValue(float value){
		text.fontSize = (int)value;
	}

}