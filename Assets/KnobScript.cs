using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobScript : MonoBehaviour, I_Manipulatable {


	Vector3 manipPreviousPos;
	float xDirect;
	public float value;
	public float valueCoeff;

	// Use this for initialization
	void Start () {
		value = 0;

	}
	public void OnManipulationStart(Vector3 position){
		manipPreviousPos = position;
	}


	public void OnManipulationUpdate(Vector3 position){
		//get the world direction of hand movement
		Vector3 worldDir = position - manipPreviousPos;

		//translate the world direction to local camera direction
		Vector3 localDir = Camera.main.transform.InverseTransformDirection(worldDir);

		//get the x axis movement, since that is all we care about
		xDirect = -localDir.x;

		//rotate the knob based on the x movement of hand in relation to camera (which is the local space x, not world space x)
		transform.Rotate (new Vector3(0, 500 * xDirect,0));

		//update the value based on how much was turned
		value += xDirect * (valueCoeff * -1f);

		manipPreviousPos = position;

	}
		
	public void OnManipulationComplete(Vector3 position){
		xDirect = 0;
	}

	public void OnManipulationCancel(){
		xDirect = 0;
	}

}
