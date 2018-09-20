using UnityEngine;

/*FIND EASIER METHOD TO SELECT THINGS (Next Semester)*/
/*GET SINE WAVE WORKING (Next Semester)*/
/*STEP-BY-STEP INSTRUCTION, TRANSISTION BETWEEN INSTRUCTIONS*/
/**/

public class WorldCursor : MonoBehaviour
{
	private MeshRenderer meshRenderer;
	private Renderer rend;
	public Material cursorRed;
	public Material cursorGreen;

	// Use this for initialization
	void Start()
	{
		// Grab the mesh renderer that's on the same object as this script.
		meshRenderer = GetComponent<MeshRenderer>();
		rend = GetComponent<Renderer> ();
	}

	// Update is called once per frame
	void Update()
	{
		// Do a raycast into the world based on the user's
		// head position and orientation.
		var headPosition = Camera.main.transform.position;
		var gazeDirection = Camera.main.transform.forward;

		RaycastHit hitInfo;

		if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
		{
			// If the raycast hit a hologram...
			// Display the cursor mesh.
			meshRenderer.enabled = true;

			// Move the cursor to the point where the raycast hit.
			transform.position = hitInfo.point;

			// Rotate the cursor to hug the surface of the hologram.
			transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

			//make the cursor green if the hologram is manipulatable, else red
			MonoBehaviour[] list = hitInfo.collider.gameObject.GetComponents<MonoBehaviour>();
			rend.material = cursorRed;
			foreach (MonoBehaviour mb in list){
				if (mb is I_Manipulatable) {
					rend.material = cursorGreen;
					break;
				}
			}
		}
		else
		{
			// If the raycast did not hit a hologram, hide the cursor mesh.
			meshRenderer.enabled = false;
		}
	}
}