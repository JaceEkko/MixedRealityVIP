using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGUIScript : MonoBehaviour {

	public GameObject canvas;

	static Material lineMaterial;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	static void CreateLineMaterial ()
	{
		if (!lineMaterial) {
			// Unity has a built-in shader that is useful for drawing
			// simple colored things.
			Shader shader = Shader.Find ("Hidden/Internal-Colored");
			lineMaterial = new Material (shader);
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			// Turn on alpha blending
			lineMaterial.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			lineMaterial.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			// Turn backface culling off
			lineMaterial.SetInt ("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
			// Turn off depth writes
			lineMaterial.SetInt ("_ZWrite", 0);
		}
	}

	// Will be called after all regular rendering is done
	public void OnRenderObject ()
	{

		CreateLineMaterial ();
		// Apply the line material
		lineMaterial.SetPass (0);

		GL.PushMatrix ();
		// Set transformation matrix for drawing to
		// match our transform
		GL.MultMatrix (transform.localToWorldMatrix);

		// Draw lines
		GL.Begin (GL.LINES);
		Vector3 pos = canvas.transform.position;// used to be canvas.gameObject.transform.position;
		Vector3 scale = canvas.gameObject.transform.localScale;
		float top = pos.y + scale.y;


		GL.Color (new Color (0.5f,0.5f,0.5f));

		//Debug.Log ("Posx: " + pos.x + " Posy: " + pos.y);

		//Vector2 lineTop = new Vector2 (pos.x - 1.5f/* - (scale.x / 2)*/, pos.y - 0.5f /*+ (scale.y / 2)*/);
		Vector2 lineTop = new Vector2 (-0.5f, 0.5f);
				
		Vector2 tmp = lineTop;
		for (int inc = 0; inc <= 10; inc++) { //this loop is for Vertical Lines
			//sts up the y axis line
			if (inc == 0) {
				GL.Color (new Color (1, 1, 1)); 
			} else {
				GL.Color (new Color (0.5f,0.5f,0.5f));
			}
			GL.Vertex3 (tmp.x , tmp.y , 0);
			//Debug.Log ("tmpx: " + tmp.x +" tmpy: "+ tmp.y);
			GL.Vertex3 (tmp.x/* - pos.x */, tmp.y - scale.y , 0);
			tmp.x += 0.1f;
		}

		//Vector2 lineTop2 = new Vector2 (pos.x - 0.5f/* - (scale.x / 2)*/, pos.y - 1.5f /*+ (scale.y / 2)*/);
		Vector2 lineTop2 = new Vector2 (0.5f, -0.5f);
		tmp = lineTop2;
		for (int inc = 0; inc <= 10; inc++) { //this loop is for Horizontal Lines
			//sets up the x axis line
			if (inc == 5) {
				GL.Color (new Color (1, 1, 1));
			} else {
				GL.Color (new Color (0.5f,0.5f,0.5f));
			}
			GL.Vertex3 (tmp.x , tmp.y , 0);
			//Debug.Log ("tmpx: " + tmp.x +" tmpy: "+ tmp.y);
			GL.Vertex3 (tmp.x - scale.x/* - pos.x */, tmp.y , 0);
			tmp.y += 0.1f;
		}

		GL.End ();
		GL.PopMatrix ();
	}
}
