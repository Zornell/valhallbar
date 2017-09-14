using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrowd : MonoBehaviour {

	public GameObject[] People;
	public int Left;
	public int Height;
	public int Length;
	public int Rows;

	private List<GameObject> peopleList = new List<GameObject> ();
	private System.Random rand = new System.Random ();

	// Use this for initialization
	void Start () {
		for (int r = 0; r < Rows; ++r) {
			for (int i = 0; i < Length; ++i) {
				int idx = rand.Next (People.GetLength (0));
				var newObj = Instantiate (People [idx]);
				float x_offset = (r % 2) * 0.5f;
				float x = Left + i + x_offset + (rand.Next(3) - 1) * 0.2f;
				float y = Height - r * 0.5f + (rand.Next(3) - 1) * 0.25f;
				newObj.transform.position = new Vector3 (x, y, 0);
				newObj.GetComponent<SpriteRenderer> ().sortingOrder = r;
				peopleList.Add (newObj);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach ( GameObject person in peopleList) {
			var threshold = rand.Next (1000);
			if (threshold < 10) {
				float y = (rand.Next (3) - 1) * 0.05f;
				person.transform.Translate(0, y, 0);
			}
		}		
	}
}
