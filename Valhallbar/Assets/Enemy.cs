using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int laneOffset;
	public int lane;
	public float height;
    public float currentHeight;
    public int time;

    public void Awake()
    {
        currentHeight = height;
    }
    
	public void Update ()
    {
        transform.position = new Vector3(lane * laneOffset, currentHeight, 0);
    }

    public void MoveTo(float pos)
    {
        currentHeight = height + pos;
    }
}
