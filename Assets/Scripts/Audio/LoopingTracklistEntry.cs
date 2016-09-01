using UnityEngine;

[RequireComponent (typeof(LoopingTrack))]
public class LoopingTracklistEntry : AudioTracklistEntry{

	private LoopingTrack cloneTrack;
	private bool usingClone = false;

	public void SetCloneTrack(LoopingTrack clone){
		cloneTrack = clone;
	}

	public override void AssignTrack (){
		base.AssignTrack ();
		((LoopingTrack) track).Clone ();
	}

	public void SwitchTracks(){
		usingClone = !usingClone;
	}

	public override ITrack GetTrack(){
		return usingClone ? cloneTrack : track;
	}

	public override bool Looping (){
		return true;
	}

}

