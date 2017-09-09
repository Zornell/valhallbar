using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject Enemy01Prefab;
	public GameObject VikingPrefab;

	const int lanes = 5;
	const int laneOffset = 2;
	const int speed = 2;

	int vikingLane = 0;
	const int vikingHeight = 4;

	Viking theViking;
	List<Enemy> enemies = new List<Enemy>();

	// Use this for initialization
	void Start () {
		theViking = Instantiate(VikingPrefab).GetComponent<Viking>();
		theViking.laneOffset = laneOffset;


		var input = (TextAsset) Resources.Load("input");
		var lines = input.text.Split(new [] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach (var line in lines)
		{
			var strings = line.Split(',');
			var time = int.Parse(strings[0]);
			var lane = int.Parse(strings[1]);
			var enemyType = int.Parse(strings[2]);

			var newObj = Instantiate (Enemy01Prefab).GetComponent<Enemy> ();
			newObj.lane = -2 + lane;
			newObj.laneOffset = 2;
			newObj.height = -time / 1000 * 2;
			enemies.Add (newObj);
		}
	}
	
	// Update is called once per frame
	void Update () {

		// move
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			theViking.move (-1);
			Debug.Log ("lane++");
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			theViking.move (1);
			Debug.Log ("lane--");
		}
			
		for (int i = 0; i < enemies.Count; ++i) {
			enemies[i].move(speed * Time.deltaTime);
			if (enemies[i].lane == theViking.lane && enemies[i].height > 3) {
				Debug.Log ("Hit!");

				Destroy (enemies [i].gameObject);
				enemies.Remove(enemies[i]);
				--i;
			}
		}
	}
}
