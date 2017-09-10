using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJainitor : MonoBehaviour {

	private bool menuLoaded = false;

	// Use this for initialization
	void Start () {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("stage",LoadSceneMode.Additive);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > 10 && Input.anyKeyDown && !menuLoaded ) {
			SceneManager.LoadScene ("mainmenu", LoadSceneMode.Additive);
			menuLoaded = true;
			Debug.Log ("loaded Mainmenu.");
		}
	}
}
