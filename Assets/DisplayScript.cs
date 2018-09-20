using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.VR.WSA.Input;	

/*GET SINE WAVE WORKING (Next Semester)*/

public class DisplayScript : MonoBehaviour {

	public double disNum;
	public double disNum2;
	public float disNumCoefficient = 1000;
	public GameObject knob;
	public GameObject knob2;
	private KnobScript knobScript;
	private KnobScript knobScriptAmp;
	//public Text disTxt;
	//public Text disTxt2;


	// Use this for initialization
	void Start () {
		disNum = 0;//this will be the num that is set to the display
		disNum2 = 0;
		knobScript = knob.GetComponent<KnobScript> ();
		knobScriptAmp = knob2.GetComponent<KnobScript> ();
	}

	
	// Update is called once per frame
	void Update () {
		//steal value from knobscript
		disNum = knobScript.value;
		disNum2 = knobScriptAmp.value;
		//disTxt.text = disNum.ToString("f2"); //force 3 numbers after decimal
		//disTxt2.text = disNum2.ToString("f2");
	}
}
