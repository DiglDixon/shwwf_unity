using UnityEngine;
using UnityEngine.Audio;


public class TrackOutputController{

//	// These two play modes could use some common refactoring...
//	public void PlayImmediate(TrackOutput nextOutput, Track nextTrack, float fadeTime){
//		SetUpcomingParameters (nextOutput, nextTrack);
//		EnterUpcomingTrackOutput (fadeTime);
//
//		if (currentOutput) {
//			currentOutput.FadeOut (fadeTime);
//		}
//		currentOutput = upcomingOutput;
//		currentOutput.SetTrack (upcomingTrack);
//		currentOutput.FadeIn(fadeTime);
//
//		if(NewOutputBeginsEvent != null)
//			NewOutputBeginsEvent (currentOutput);
//	}
//
//	public void PlayAfterCurrent(TrackOutput nextOutput, ITrack nextTrack, float fadeTime){
//		SetUpcomingParameters (nextOutput, nextTrack);
//	}
//
//	protected virtual void EnterUpcomingTrackOutput(float fadeTime){
//	} 
//
//	private void SetUpcomingParameters(TrackOutput output, ITrack track){
//		Diglbug.Log ("Set next output parameters " + output.name+" with "+track.GetTrackName(), PrintStream.AUDIO_GENERAL);
//		upcomingOutput = output;
//		upcomingTrack = track;
//	}

}