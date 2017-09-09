using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject EnemyPrefab;

    // Use this for initialization
	void Start () {
	    var input = (TextAsset) Resources.Load("input");
	    var lines = input.text.Split(new [] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
	    foreach (var line in lines)
	    {
	        var strings = line.Split(',');
	        var time = int.Parse(strings[0]);
	        var lane = int.Parse(strings[1]);
	        var enemyType = int.Parse(strings[2]);

	        var laneOffset = 2;
            
            var newObj = Instantiate(EnemyPrefab);
            newObj.transform.SetParent(transform);
	        newObj.transform.position = new Vector3(-4 + laneOffset * lane, -time/1000*2);
	    }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
