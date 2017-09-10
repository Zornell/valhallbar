using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneJainitor : MonoBehaviour {

	private bool menuLoaded = false;

	// Use this for initialization
	void Start () {
        SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
		SceneManager.LoadScene ("stage",LoadSceneMode.Additive);
	}

    private void SceneManagerOnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "mainmenu")
        {
            var component = GameObject.Find("Start").GetComponent<Button>();
            component.onClick.AddListener(StartGame);
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("tavern", LoadSceneMode.Single);
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
