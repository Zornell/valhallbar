using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float Speed = 4;

    // Use this for initialization
	void Start () {
	    var input = (TextAsset) Resources.Load("technoviking_all");
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
	        newObj.transform.position = new Vector3(-4 + laneOffset * lane, (-time * Speed/1000f) + 1.5f );
	        var behaviour = newObj.GetComponent<MoveUpwards>();
	        behaviour.Speed = Speed;
	    }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
