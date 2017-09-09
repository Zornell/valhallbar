using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int laneOffset;
	public int lane;
	public float height;

	public void Move (float heightDelta)
    {
		height += heightDelta; 
	}

	public void Update ()
    {
		transform.position = new Vector3 (lane * laneOffset, height, 0);
	}

}
