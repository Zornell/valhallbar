using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOver : MonoBehaviour {
    private float _startTime;

    // Use this for initialization
	void Start ()
	{
	    _startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var timeDiff = Time.time - _startTime;
	    if (timeDiff > 5 || timeDiff > 1 && Input.anyKeyDown)
	    {
	        SceneManager.LoadScene("main", LoadSceneMode.Single);
	    }
	}
}
