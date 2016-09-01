using System.Collections;

// GENERATED CODE.
public class TrackedBundleVersion
{
	public static readonly string bundleIdentifier = "com.storybox.twwf2016";

	public static readonly TrackedBundleVersionInfo Version_5_4 =  new TrackedBundleVersionInfo ("5.4", 0);
	
	public static readonly TrackedBundleVersion Instance = new TrackedBundleVersion ();

	public static TrackedBundleVersionInfo Current { get { return Instance.current; } }

	public static int CurrentAndroidBundleVersionCode { get { return 2; } }

	public ArrayList history = new ArrayList ();

	public TrackedBundleVersionInfo current = Version_5_4;

	public  TrackedBundleVersion() {
		history.Add (current);
	}

}
