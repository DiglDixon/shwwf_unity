using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class DebugStreams : MonoBehaviour{

	public bool updateStructure = false;

	public bool startWithAll = false;

	public bool loadOptions = false;

	public UILightbox optionsLightbox;
	public GameObject optionsGridParent;
	public GameObject optionObject;

	public DebugStreamOption[] allOptions;

	private void Start(){
		if (startWithAll) {
			EnableAll ();
		} else {
			DisableAll ();
		}
	}

	public void Update(){
		if (Input.GetKeyDown (KeyCode.Alpha9)) {
			optionsLightbox.Open ();
		}
	}

	public void OptionPressed(DebugStreamOption option, bool value){
		if (value) {
			Diglbug.EnablePrintStream (option.stream);
		} else {
			Diglbug.DisablePrintStream (option.stream);
		}
	}

	public void DisableAll(){
		for (int k = 0; k < allOptions.Length; k++) {
			allOptions [k].ForceValue (false);
		}
	}

	public void EnableAll(){
		for (int k = 0; k < allOptions.Length; k++) {
			allOptions [k].ForceValue (true);
		}
	}

	private void OnValidate(){
		updateStructure = false;

		int enumCount = Enum.GetNames (typeof(PrintStream)).Length;
		while (optionsGridParent.transform.childCount < enumCount) {
			GameObject newChild = GameObject.Instantiate (optionObject);
			newChild.transform.SetParent (optionsGridParent.transform, false);
			DebugStreamOption option = newChild.GetComponent<DebugStreamOption> ();
			option.callback = this;
		}


		if (loadOptions) {
			loadOptions = false;
			GameObject cChild;
			DebugStreamOption cOption;
			PrintStream cStream;
			allOptions = new DebugStreamOption[enumCount];
			for (int k = 0; k < enumCount; k++) {
				cChild = optionsGridParent.transform.GetChild (k).gameObject;
				cStream = ((PrintStream)k);
				cChild.name = cStream.ToString ();
				cOption = cChild.GetComponent<DebugStreamOption> ();
				cOption.stream = cStream;
				allOptions [k] = cOption;
				cChild.GetComponentInChildren<Text>().text = cStream.ToString ();
			}
		}


		for (int k = enumCount; k < optionsGridParent.transform.childCount; k++) {
			optionsGridParent.transform.GetChild (k).gameObject.name = "__UNUSED";
		}

	}

}