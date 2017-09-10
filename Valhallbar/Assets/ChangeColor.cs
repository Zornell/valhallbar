using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class ChangeColor : MonoBehaviour
{
    
    public bool R;
    public bool G;
    public bool B;

    private Light _light;
    private float _offset;
    private float _frequency;

    // Use this for initialization
    void Start ()
	{
	    _light = GetComponent<Light>();
	    _offset = Random.value * Mathf.PI;
	    _frequency = Random.Range(1, 3);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var r = R ? Mathf.Sin(_frequency * Time.time + _offset) : 0;
	    var g = G ? Mathf.Sin(_frequency * Time.time + _offset) : 0;
	    var b = B ? Mathf.Sin(_frequency * Time.time + _offset) : 0;

	    _light.color = new Color(r, g, b);
	}
}
