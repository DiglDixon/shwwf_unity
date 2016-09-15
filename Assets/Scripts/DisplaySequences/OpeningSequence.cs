

public class OpeningSequence : SequenceWithBackground{
	
	private void Start(){
		if (!ShowMode.Instance.HasRunOpening) {
			Begin ();
			ShowMode.Instance.HasRunOpening = true;
		} else {
			gameObject.SetActive (false);
		}
	}

}
