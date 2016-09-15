using UnityEngine;
//using UnityEngine.SceneManagement;

/// <summary>
/// A modification of the Singleton.cs class found at http://wiki.unity3d.com/index.php/Singleton
/// 
/// That version didn't persist variables. Or at all, really.
/// 
/// This one can be placed within a scene and it'll survive across scenes.
/// This allows things like Update loops to run without needing an Instance call.
/// 
/// It will also be created on Instance call, if not already present, and run from
/// then on.
/// 
/// </summary>
public class ConstantSingleton<T> : MonoBehaviour where T : MonoBehaviour{
	private static T _instance;

	protected virtual void Awake(){
		if (_instance == null) {
			SetInstance (gameObject);
		} else {
			if (_instance.gameObject == gameObject) {
				Debug.Log ("Found self as existing singleton: "+name);
			} else {
				Debug.Log ("Found foreign existing singleton. Removed self "+name);
				Destroy (gameObject);
			}
		}
	}


	public static T Instance
	{
		get
		{

//			if (applicationIsQuitting) {
//				Debug.LogWarning("[Singleton] Instance '"+ typeof(T) +
//					"' already destroyed on application quit." +
//					" Won't create again - returning null.");
//				return null;
//			}
			if (_instance == null)
			{
				_instance = (T) FindObjectOfType(typeof(T));

				if (_instance == null)
				{
					Diglbug.LogError ("ConstantSingleton not previously instantiated - these should be spawns and DDOLed. ");
					//CreateInstance ();
				} else {
					Debug.Log("[Singleton] Using instance already created: " +
						_instance.gameObject.name);
				}
			}

			return _instance;
		}
	}

	public static void SetInstance(GameObject existing){
		existing.name = "(singleton) "+ typeof(T).ToString();
		_instance = existing.GetComponent<T> ();

		DontDestroyOnLoad(existing);
	}

//	private static void CreateInstance(){
//		Debug.Log ("Dynamically created Singleton");
//		GameObject singleton = new GameObject();
//		singleton.AddComponent<T>();
//		SetInstance (singleton);
//	}

//	private static bool applicationIsQuitting = false;
	/// <summary>
	/// When Unity quits, it destroys objects in a random order.
	/// In principle, a Singleton is only destroyed when application quits.
	/// If any script calls Instance after it have been destroyed, 
	///   it will create a buggy ghost object that will stay on the Editor scene
	///   even after stopping playing the Application. Really bad!
	/// So, this was made to be sure we're not creating that buggy ghost object.
	/// </summary>
//	public void OnDestroy () {
//		applicationIsQuitting = true;
//	}
}