using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightScript : MonoBehaviour {

	private DisplayScript displayScript;
	public GameObject disObj;
	public Material red;
	public Material green;
	private Renderer rend;

	// Use this for initialization
	void Start () {
		displayScript = disObj.GetComponent<DisplayScript> ();
		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(displayScript.disNum > 3){
			rend.material = red;
		}
		else{
			rend.material = green;
		}
	}
}
