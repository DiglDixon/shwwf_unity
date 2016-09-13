using UnityEngine.SceneManagement;

public class ReturnToMenuEvent : CustomTrackTimeEvent{

	public override void CustomEvent (){
		SceneManager.LoadScene (Scenes.MainMenu);
	}

}