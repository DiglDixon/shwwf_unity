using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowMode : ConstantSingleton<ShowMode>{

	public Mode startingMode;
	public Mode Mode{ get; private set; }

	private Signature _Signature;
	public Signature Signature {
		get {
			return _Signature;
		}
		set {
			Diglbug.Log("Setting signature to: "+value, PrintStream.SIGNALS);
			_Signature = value;
			BLE.Instance.Manager.SetSendingSignature (Signature);
			BLE.Instance.Manager.SetReceivedSignature (Signature);
			RecoveryManager.Instance.SignatureSet (Signature);
		}
	}

	private Mode[] possibleModes;

	private DebugCommand[] commands;

	protected void Start(){
		Mode = startingMode;
		possibleModes = GetComponentsInChildren<Mode> ();
		commands = GetComponentsInChildren<DebugCommand> ();
		SceneManager.sceneLoaded += NewSceneLoaded;
	}

	public bool HasRunOpening = false;

	private void NewSceneLoaded(Scene scene, LoadSceneMode loadMode){
		Diglbug.Log ("New scene begins: " + scene.name, PrintStream.SCENES);
		Mode.NewSceneLoaded (scene);
	}

	public void PasswordEntered(string p){
		SetModeByPassword (p);
		RunCommandsFromPassword (p);
	}

	public bool PasswordIsValid(string password){
		return GetModeByPassword (password) != null;
	}

	private void SetModeByPassword(string password){
		Mode newMode = GetModeByPassword (password);
		if (newMode != null) {
			Diglbug.Log ("Successfully set mode to " + newMode.ModeName + " with password " + password, PrintStream.MODES);
			SetCurrentMode (newMode);
		} else {
			Diglbug.Log ("Mode with password " + password + " not found!", PrintStream.MODES);
		}
	}

	private void RunCommandsFromPassword(string p){
		for (int k = 0; k < commands.Length; k++) {
			if (commands [k].isValidCommand (p)) {
				commands [k].RunCommand (p);
			}
		}
	}

	public void SetMode(ModeName modeName){
		Mode newMode = GetModeByName (modeName);
		if (newMode != null) {
			SetCurrentMode (newMode);
		} else {
			Diglbug.LogError ("Mode with name " + modeName + " not found!");
		}
	}

	private Mode GetModeByName(ModeName modeName){
		foreach (Mode m in possibleModes) {
			if (m.ModeName == modeName) {
				return m;
			}
		}
		return null;
	}

	private Mode GetModeByPassword(string password){
		Mode m;
		for(int k = 0; k<possibleModes.Length; k++){
			m = possibleModes [k];
			if (m.PasswordIsCorrect (password)) {
				return m;
			}
		}
		return null;
	}

	private void SetCurrentMode(Mode m){
		if (Mode != null) {
			Mode.ModeDeselected ();
		}
		Mode = m;
		Mode.ModeSelected ();
		RecoveryManager.Instance.SetMode (m.ModeName);
		Diglbug.Log ("Set Mode to " + Mode.ModeName, PrintStream.MODES);
	}

}