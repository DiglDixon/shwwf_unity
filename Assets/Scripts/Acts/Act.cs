using UnityEngine;

[RequireComponent (typeof(Tracklist))]
public class Act : MonoBehaviour{

	public string ActName;
	public Tracklist tracklist;

	public void Begin(){

	}


	/* Need:
	 * - Some sort of GetLength() and Skip()
	 * - Need to be careful of state events when skipping.
	 * 
	 * An ActPlayer.
	 * 
	 * */

}