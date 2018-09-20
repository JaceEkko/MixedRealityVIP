using UnityEngine;
using UnityEngine.XR.WSA.Input;


public class GazeGestureManager : MonoBehaviour
{
	public static GazeGestureManager Instance { get; private set; }

	// Represents the hologram that is currently being gazed at.
	public GameObject FocusedObject { get; private set; }
	public GameObject HeldObject;

	GestureRecognizer recognizer;

	// Use this for initialization
	void Awake()
	{
		Instance = this;

		// Set up a GestureRecognizer to detect Select gestures.
		recognizer = new UnityEngine.XR.WSA.Input.GestureRecognizer();

		//what happens when a tap is recognized
		recognizer.TappedEvent += (source, tapCount, ray) =>
		{
			// Send an OnSelect message to the focused object and its ancestors.
			if (FocusedObject != null)
			{
				FocusedObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
			}
		};

		//some redundancy with heldObject and FocusedObject, need to redo later
		//since often times HeldObject and FocusedObject are the same thing, just want to clear confusion
		recognizer.HoldStartedEvent += (UnityEngine.XR.WSA.Input.InteractionSourceKind source, Ray headRay) => {
			if (FocusedObject != null){
				FocusedObject.SendMessageUpwards("OnHoldStarted", SendMessageOptions.DontRequireReceiver);
				HeldObject = FocusedObject;
			}
		};
		recognizer.HoldCompletedEvent += (UnityEngine.XR.WSA.Input.InteractionSourceKind source, Ray headRay) => {
			HeldObject = null;
			if (FocusedObject != null)
				FocusedObject.SendMessageUpwards("OnHoldCompleted", SendMessageOptions.DontRequireReceiver);
			
		};
		recognizer.HoldCanceledEvent += (UnityEngine.XR.WSA.Input.InteractionSourceKind source, Ray headRay) => {
			HeldObject = null;
			if (FocusedObject != null) 
				FocusedObject.SendMessageUpwards("OnHoldCanceled", SendMessageOptions.DontRequireReceiver);
			
		};

		//what happens when a manipulation (a pinch then hand movement) is detected
		recognizer.ManipulationStartedEvent += (UnityEngine.XR.WSA.Input.InteractionSourceKind source, Vector3 handPosition, Ray headRay) => {
			if (FocusedObject != null) {
				HeldObject = FocusedObject;
				HeldObject.SendMessageUpwards("OnManipulationStart", handPosition, SendMessageOptions.DontRequireReceiver);
			}
		};
		recognizer.ManipulationUpdatedEvent += (UnityEngine.XR.WSA.Input.InteractionSourceKind source, Vector3 handPosition, Ray headRay) => {
			HeldObject.SendMessageUpwards("OnManipulationUpdate", handPosition, SendMessageOptions.DontRequireReceiver);
		};
		recognizer.ManipulationCompletedEvent += (UnityEngine.XR.WSA.Input.InteractionSourceKind source, Vector3 handPosition, Ray headRay) => {
			HeldObject.SendMessageUpwards("OnManipulationComplete", handPosition, SendMessageOptions.DontRequireReceiver);
			HeldObject = null;
		};
		recognizer.ManipulationCanceledEvent += (UnityEngine.XR.WSA.Input.InteractionSourceKind source, Vector3 handPosition, Ray headRay) => {
			HeldObject.SendMessageUpwards("OnManipulationCancel", handPosition, SendMessageOptions.DontRequireReceiver);
			HeldObject = null;
		};
		HeldObject = null;

		recognizer.StartCapturingGestures();


	}

	// Update is called once per frame
	void Update()
	{
		// Figure out which hologram is focused this frame.
		GameObject oldFocusObject = FocusedObject;

		// Do a raycast into the world based on the user's head position and orientation.
		var headPosition = Camera.main.transform.position;
		var gazeDirection = Camera.main.transform.forward;

		RaycastHit hitInfo;
		if (Physics.Raycast(headPosition, gazeDirection, out hitInfo) && HeldObject == null)
		{
			// If the raycast hit a hologram and we dont have a heldObject, use that as the focused object.
			FocusedObject = hitInfo.collider.gameObject;
		}
		else
		{
			//Keep the held object as the focused object
			FocusedObject = HeldObject;
		}

		// If the focused object changed this frame,
		// start detecting fresh gestures again.
		if (FocusedObject != oldFocusObject)
		{
			recognizer.CancelGestures();
			recognizer.StartCapturingGestures();
			recognizer.SetRecognizableGestures(UnityEngine.XR.WSA.Input.GestureSettings.Tap | UnityEngine.XR.WSA.Input.GestureSettings.Hold | UnityEngine.XR.WSA.Input.GestureSettings.ManipulationTranslate);
		}
	}
}