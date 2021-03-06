using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

//#if NETFX_CORE
//using Windows.Storage;
//#endif
/**
 * Add a reference to this assembly:
C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework.NETCore\v4.5.1\System.Runtime.WindowsRuntime.dll
 * */
public class GraphScript : MonoBehaviour

{
	// When added to an object, draws colored rays from the
	// transform position.
	//public int lineCount = 10;
	public KnobScript xKnobScale;
	public KnobScript yKnobScale;

	public KnobScript xKnobShift;
	public KnobScript yKnobShift;

	public KnobScript xKnobBound;
	public KnobScript yKnobBound;

	public float yScale;
	public float xScale;
	public float yShift = 0.0f;
	public float xShift = 0.0f;
	public float xBoundary = 10f;
	public float yBoundary = 10f;
	public float nxBoundary = -0.01f;
	public float nyBoundary = -0.01f;

	public Text ampLo;
	public Text ampHi;

	public GameObject canvas;



	//public GameObject Canvas;

	//public string path = "Assets/GraphScripts/test.txt";
	//private FileReader myReader;


	static Material lineMaterial;

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

	List<Vector2> ReadFile(){ //reads a file that contains the points needed to create the graph

		List<Vector2> coords = new List<Vector2>();

		//string path = Path.Combine (Application.persistentDataPath, "test.txt");
		TextAsset path1 = Resources.Load("test") as TextAsset;

		string path = path1.ToString();
		//#if !NETFX_CORE
		//StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open)); //used to be FileMode.Open
		//string file = reader.ReadToEnd();
		string[] coordsAsString = path.Split ('\n');
		string pair;
		int i = 0;

		foreach ( string p in coordsAsString)/*while(!reader.EndOfStream)*/{ //loop through file data and retrieve the two coordinate points
			pair = p;
			pair = coordsAsString [i];
			string[] pairAsString = pair.Split (',');
			float x = float.Parse (pairAsString [0]);
			float y = float.Parse (pairAsString [1]);
			//Debug.Log ("x: " + x + " y: " + y);
			//print(x.ToString() + " " + y.ToString());
			Vector2 toAdd = new Vector2 (x, y);
			coords.Add (toAdd);
			i++;
		}
		//reader.Dispose ();
		//Debug.Log(reader.EndOfStream);


		//#else
		//Get local folder          
		//StorageFolder storageFolder = ApplicationData.Current.LocalFolder;                       
		//Get file          
		//StorageFile textFileForRead = storageFolder.GetFileAsync("test.txt");          
		//Read file          
		//string plainText = "";          
		//plainText = FileIO.ReadTextAsync(textFileForRead);          
		//Debug.Log("New file written: " + plainText);
		//#endif

		return coords;
		//______________________________________________________________
//		List<Vector2> coords = new List<Vector2>();
//		coords.Add (new Vector2 (0, 0));
//		coords.Add (new Vector2 (0.1f,0.099833417f));
//		coords.Add (new Vector2 (0.2f,0.198669331f));
//		coords.Add (new Vector2 (0.3f,0.295520207f));
//		coords.Add (new Vector2 (0.4f,0.389418342f));
//		coords.Add (new Vector2 (0.5f,0.479425539f));
//		coords.Add (new Vector2 (0.6f,0.564642473f));
		//________________________________________________________________
		//
	}

	// Will be called after all regular rendering is done
	public void OnRenderObject ()
	{
		xScale = 1 + xKnobScale.value;
		yScale = 1 + yKnobScale.value;
		xShift = xKnobShift.value;
		yShift = yKnobShift.value;

		Vector3 high = new Vector3 (canvas.transform.position.x - 0.4f, canvas.transform.position.y + (yScale+yShift)*(0.1f) /*+1f*/, canvas.transform.position.z - 0.1f);
		ampHi.transform.position = high;
		Vector3 low = new Vector3 (canvas.transform.position.x - 0.4f, canvas.transform.position.y + (yShift-yScale)*(0.1f) /*+1f*/, canvas.transform.position.z - 0.1f);
		ampLo.transform.position = low;
		ampHi.text = (yScale + yShift).ToString();
		ampLo.text = (yShift - yScale).ToString();
		CreateLineMaterial ();
		// Apply the line material
		lineMaterial.SetPass (0);

		GL.PushMatrix ();
		// Set transformation matrix for drawing to
		// match our transform
		GL.MultMatrix (transform.localToWorldMatrix);

		// Draw lines
		GL.Begin (GL.LINES);
		float prevX = 0.0f + xShift;
		float prevY = 0.0f + yShift;
		float i = 0.0f;

		GL.Color (new Color (1, 1, 0));

		List<Vector2> coords = ReadFile ();
		//Canvas.transform.localScale = new Vector2 (xScale, yScale);
		foreach (Vector2 pair in coords) {
			//float y = Mathf.Sin (i) * yScale;
			float y = (pair.y * yScale) + yShift;
			float x = (pair.x * xScale) + xShift;

			//float x = i * xScale;
			//Debug.Log ("x: " + x + " y: " + y);

			if (Mathf.Abs(x) > xBoundary || Mathf.Abs(y) > yBoundary) {
				//i += 0.1f;
				prevX = x;
				prevY = y;
				//GL.Vertex3 (prevX , prevY , 0);
				continue;
			}
//			if (Mathf.Abs(x) > nxBoundary || Mathf.Abs(y) < nyBoundary) {//negative ends of the graph
//				//i += 0.1f;
//				prevX = x;
//				prevY = y;
//				//GL.Vertex3 (prevX , prevY , 0);
//				continue;
//			}


			// Vertex colors change from red to green
			//GL.Color (new Color (1, 1, 0/*, 0.8F*/));//yellow color
			// One vertex at transform position
			//GL.Vertex3(0, 0, 0);
			GL.Vertex3 (prevX , prevY , 0);
			// Another vertex at edge of circle
			GL.Vertex3 (x,  y , 0);
			prevX = x;
			prevY = y;
			//i += 0.1f;

		}
		GL.End ();
		GL.PopMatrix ();
	}
}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class GraphScript : MonoBehaviour {
//
//	// Use this for initialization
//	void Start () {
//
//	}
//
//	// Update is called once per frame
//	void Update () {
//
//	}
//}
