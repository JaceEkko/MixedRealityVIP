using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphManipScript : MonoBehaviour, I_Manipulatable {

	Vector3 manipPreviousPos;

	Rigidbody rb;

	Vector3 Direction;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	public void OnManipulationStart(Vector3 position){
		//OLD WAY__________________________________________________________________________________
//		//get the world direction of hand movement
//		Vector3 worldDir = position - manipPreviousPos;
//
//		//translate the world direction to local camera direction
//		Vector3 localDir = Camera.main.transform.InverseTransformDirection(worldDir);
//
//		//get the axis movements
//		Direction.x = localDir.x;
//		Direction.y = localDir.y;
//		Direction.z = localDir.z;
//
//		transform.SetPositionAndRotation (new Vector3 (Direction.x, Direction.y, Direction.z), new Quaternion(0,0,0,0));
//		//transform.TransformPoint(Direction.x, Direction.y, Direction.z);
//
//		manipPreviousPos = position;
		//__________________________________________________________________________________________

		manipPreviousPos = position;
	}


	public void OnManipulationUpdate(Vector3 position){
		
		//calculates the direction your hand moved between previous frame and this frame
		Vector3 forceVec = (position - manipPreviousPos);

		//adds a force to the dice equal to that direction times some force number
		rb.AddForce(forceVec * 1000f);
		manipPreviousPos = position;
	}

	public void OnManipulationComplete(Vector3 position){
		Direction.x = 0;
		Direction.y = 0;
		Direction.z = 0;
	}

	public void OnManipulationCancel(){
		Direction.x = 0;
		Direction.y = 0;
		Direction.z = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
