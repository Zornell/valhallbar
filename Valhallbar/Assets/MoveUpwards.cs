using UnityEngine;

public class MoveUpwards : MonoBehaviour
{
    public float Speed = 2;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    this.transform.Translate(0, Speed * Time.deltaTime, 0);
	}
}
