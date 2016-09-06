using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowMode : ConstantSingleton<ShowMode>{

	public Mode startingMode;
	private Mode mode;

	public Signature Signature{ get; set; }

	private Mode[] possibleModes;

	protected void Start(){
		mode = startingMode;
		possibleModes = GetComponentsInChildren<Mode> ();
		SceneManager.sceneLoaded += NewSceneLoaded;
	}

	private void NewSceneLoaded(Scene scene, LoadSceneMode loadMode){
		Diglbug.Log ("New scene begins: " + scene.name, PrintStream.SCENES);
		mode.NewSceneLoaded (scene);
	}

	private void Update(){
		if (Input.GetKeyDown (KeyCode.Equals)) {
			Diglbug.Log ("Equals-returned to main_menu", PrintStream.DEBUGGING);
			SceneManager.LoadScene (Scenes.MainMenu);
		}
	}

	public bool PasswordIsValid(string password){
		return (GetModeByPassword (password) != null);
	}

	public void SetModeByPassword(string password){
		Mode newMode = GetModeByPassword (password);
		if (newMode != null) {
			Diglbug.Log ("Successfully set mode to " + newMode.ModeName + " with password " + password, PrintStream.MODES);
			SetCurrentMode (newMode);
		} else {
			Diglbug.Log ("Mode with password " + password + " not found!", PrintStream.MODES);
		}
	}

	public void SetMode(string modeName){
		Mode newMode = GetModeByName (modeName);
		if (newMode != null) {
			SetCurrentMode (newMode);
		} else {
			Diglbug.LogError ("Mode with name " + modeName + " not found!");
		}
	}

	private Mode GetModeByName(string modeName){
		foreach (Mode m in possibleModes) {
			if (m.ModeName.Equals (modeName)) {
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
		if (mode != null) {
			mode.ModeDeselected ();
		}
		mode = m;
		mode.ModeSelected ();
	}

}