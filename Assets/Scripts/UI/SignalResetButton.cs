using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class SignalResetButton : MonoBehaviour {

	public ActorPlayer actorPlayer;
	public Text textToChange;

	private Button button;

	private void Start(){
		button = GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
		int signalCount = actorPlayer.PreviousSignalsCount();
		if (signalCount == 0) {
			button.interactable = false;
			textToChange.text = "Received signals cleared";
		} else {
			button.interactable = true;
			textToChange.text = "Clear ("+signalCount+") Received Signals";
		}
	}
}
