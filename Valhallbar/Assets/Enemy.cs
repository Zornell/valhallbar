using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int laneOffset;
	public int lane = 0;
	public int lanes;
	public float height;

	public void move (float heightDelta) {
		height += heightDelta; 
	}

	public void Update () {
		this.transform.position = new Vector3 (lane * laneOffset, height, 0);
	}

}
