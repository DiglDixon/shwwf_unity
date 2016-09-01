using UnityEngine;

public class VideoPlaybackSystem : WrappedTrackOutput{
	
	private TrackOutput player;

	protected override TrackOutput WrappedOutput{
		get{
			return player;
		}
	}

	public TrackOutput GetPlayer(){
		return WrappedOutput;
	}

	private void Awake(){
		GameObject playerObject;
		#if UNITY_EDITOR
		playerObject = GameObject.Instantiate(Resources.Load("Desktop_Video_Player")) as GameObject;
		#else
		playerObject = GameObject.Instantiate(Resources.Load("Mobile_Video_Player")) as GameObject;
		#endif
		playerObject.transform.SetParent(transform, false);
		player = playerObject.GetComponent<VideoPlayer>();
	}

}