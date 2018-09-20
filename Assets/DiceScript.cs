using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour, I_Manipulatable
{

	public Collider border;

	private Rigidbody rb;
	private AudioSource source;
	private Behaviour halo;
	private float nudgeRate = 1f;
	private float nextNudgeTime;
	private bool isHolding;
	private Material mat;

	private Vector3 manipulationPreviousPosition;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		source = GetComponent<AudioSource> ();
		halo = (Behaviour) GetComponent ("Halo");

		//mat = GetComponent<Renderer> ().materials [1];
		nextNudgeTime = Time.time;

		isHolding = false;

	}

	void Update ()
	{
		
	}

	//play sound if dice hits a surface
	void OnCollisionEnter ()
	{
		source.pitch = Random.Range (.8f, 1.2f);
		source.Play ();
	}

	//destroy dice if it exits the border
	void OnTriggerExit(Collider other){
		if (other.CompareTag ("border")) {
			Destroy (this.gameObject);
		}
	}

	void OnMouseDown ()
	{
		if (Time.time > nextNudgeTime) {
			Nudge ();
			nextNudgeTime += nudgeRate;
		}
	}
		
	// Called by GazeGestureManager when the user performs a Select gesture
	void OnSelect ()
	{
		if (!isHolding)
			Nudge ();

	}

	//saves the first position, prepares dice properties for easier movement
	public void OnManipulationStart(Vector3 position){
		manipulationPreviousPosition = position; 
		rb.useGravity = false;
		rb.drag = 3;
		halo.enabled = true;
	}

	//moves dice the same direction your hand moves
	public void OnManipulationUpdate(Vector3 position){
		
		//calculates the direction your hand moved between previous frame and this frame
		Vector3 forceVec = (position - manipulationPreviousPosition);

		//adds a force to the dice equal to that direction times some force number
		rb.AddForce(forceVec * 1000f);
		manipulationPreviousPosition = position;
	
	}

	//reverts dice to normal properties
	public void OnManipulationComplete(Vector3 position){
		rb.useGravity = true;
		rb.drag = 0;
		halo.enabled = false;
	}

	public void OnManipulationCancel(){
		rb.useGravity = true;
		rb.drag = 0;
		halo.enabled = false;
	}

	//is called when a hold gesture is triggered on the dice
//	void OnHoldStarted()
//	{
//		print ("OnHoldStarted an object");
//		isHolding = true;
//		holdCursor = new GameObject ("hold cursor");
//		//GetComponent<BoxCollider> ().enabled = false;
//		rb.useGravity = false;
//		rb.drag = 5;
//		//rb.isKinematic = true;
//		halo.enabled = true;
//
//		holdCursor.transform.position = transform.position;
//		holdCursor.transform.SetParent (Camera.main.transform);//held object follow your head
//		//holdCursor.transform.SetParent (Camera.main.transform.forward);
//
//	}
//
//	//is called when a hold gesture is completed on the dice
//	void OnHoldCompleted(){
//		print ("OnHoldCompleted an object");
//		//GetComponent<BoxCollider> ().enabled = true;
//		rb.useGravity = true;
//		rb.drag = 0;
//		isHolding = false;
//		halo.enabled = false;
//	}
//
//	void OnHoldCanceled(){
//		OnHoldCompleted ();
//	}

	void Nudge ()
	{
		rb.AddForce (Vector3.up * 50);
		rb.AddForce (Vector3.left * Random.Range (-20, 20));
		rb.AddForce (Vector3.forward * Random.Range (-20, 20));
		rb.AddTorque (Vector3.left * Random.Range (-20, 20));
		rb.AddTorque (Vector3.forward * Random.Range (-20, 20));


	}
}
