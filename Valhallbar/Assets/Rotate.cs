using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private float _freq;
    public float Direction = 1;

    // Use this for initialization
	void Start ()
	{
	    _freq = Random.Range(50, 200) * Direction;
	}
	
	// Update is called once per frame
	void Update ()
    {
		transform.Rotate(Vector3.forward, Time.deltaTime * _freq);
	}
}
