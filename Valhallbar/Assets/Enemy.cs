using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int laneOffset;
	public int lane;
	public float height;
    public float currentHeight;
    public int time;
	private SpriteRenderer _sr;
	private float deltaTime = 0;

    public void Awake()
    {
        currentHeight = height;
		_sr = GetComponent<SpriteRenderer> ();
    }
    
	public void Update ()
    {
        transform.position = new Vector3(lane * laneOffset, currentHeight, 0);
		deltaTime += Time.deltaTime;
		if (deltaTime > 0.25f) {
			_sr.flipX = ! _sr.flipX;
			deltaTime = 0.0f;
		}
    }

    public void MoveTo(float pos)
    {
        currentHeight = height + pos;
    }
}
