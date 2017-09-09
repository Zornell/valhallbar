using System;
using UnityEngine;

public class Viking : MonoBehaviour {

	public const int height = 4;
	public int laneOffset;
	public int lanes;
	public int lane = 0;

	public void move (int laneDelta) {
		var newlane = lane + laneDelta;
		if ( newlane < 3 && newlane > - 3)
			lane = newlane; 
	}

	public void Update () {
		this.transform.position = new Vector3 (lane * laneOffset, height, 0);
	}

}
