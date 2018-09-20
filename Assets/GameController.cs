using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController self;

	public Transform spawn;
	public GameObject dice;
	public bool autospawn = false;

	private Vector3 baseSpawnPoint;
	private float spawnRate = .5f;
	private float nextSpawnTime;
	private List<GameObject> dieList;

	public float autospawnRate = 5f;
	public float autospawnNextSpawnTime;

	public Transform cursor;

	void Awake(){
		self = this;
	}

	void Start () {
		baseSpawnPoint = spawn.position;
		nextSpawnTime = Time.time;
		autospawnNextSpawnTime = Time.time;
		dieList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && Time.time > nextSpawnTime) {
			SpawnDice ();
			nextSpawnTime += spawnRate;
		} else if (Input.GetKeyDown (KeyCode.C)) {
			ClearDice ();
		}
		if (autospawn && Time.time > autospawnNextSpawnTime) {
			SpawnDice ();
			autospawnNextSpawnTime += autospawnRate;
			if (dieList.Count > 10) {
				ClearDice ();
			}
		}
	}

	public void SpawnDice(){
		Vector3 spawnPoint = new Vector3 (
			                     baseSpawnPoint.x + Random.Range (-.1f, 0f),
			                     baseSpawnPoint.y + Random.Range (-.1f, .1f),
			                     baseSpawnPoint.z + Random.Range (-.3f, .3f));
		
		GameObject clone = Instantiate (dice, spawnPoint, Random.rotation) as GameObject;
		dieList.Add (clone);


	}
	public void ClearDice(){
		for (int i = 0; i < dieList.Count; i++) {
			Destroy (dieList [i]);
		}
		dieList.Clear ();
	}
}
