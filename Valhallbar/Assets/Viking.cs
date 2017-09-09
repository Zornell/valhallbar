using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : MonoBehaviour {

	int lane = 3;
	int laneOffset = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Input.anyKeyDown);

		if (Input.GetKeyDown (KeyCode.LeftArrow) && lane > 1) {
			lane -= 1;
			Debug.Log ("lane++");
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && lane < 5) {
			lane += 1;
			Debug.Log ("lane--");
		}

		this.transform.position = new Vector3(- 3*laneOffset + lane*laneOffset, 4, 0);
	}
}
