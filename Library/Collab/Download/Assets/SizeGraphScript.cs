using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeGraphScript : MonoBehaviour, I_Manipulatable {

	Vector3 manipPreviousSize;
	Vector3 manipPreviousPos;


	public GameObject ARGraph;



	// Use this for initialization
	void Start () {
		
	}
	
	public void OnManipulationStart(Vector3 position){
		manipPreviousSize = ARGraph.transform.localScale;
		manipPreviousPos = position;
	}


	public void OnManipulationUpdate(Vector3 position){
//		//get the world direction of hand movement
		Vector3 worldDir = position - manipPreviousPos;

		//translate the world direction to local camera direction
		Vector3 localDir = Camera.main.transform.InverseTransformDirection(worldDir);

//		//get the x axis movement, since that is all we care about
//		xDirect = -localDir.x;
//
//		//rotate the knob based on the x movement of hand in relation to camera (which is the local space x, not world space x)
		//transform.localScale (new Vector3(0, 500 * xDirect,0));
//
//		//update the value based on how much was turned
//		value += xDirect * (valueCoeff * -1f);
//
//		manipPreviousPos = position;

		//Vector3 size = manipPreviousSize(new Vector3())//figuring out what command to say to manjpulate the size when box is dragged

	}

	public void OnManipulationComplete(Vector3 position){
		//xDirect = 0;
	}

	public void OnManipulationCancel(){
		//xDirect = 0;
	}
}
