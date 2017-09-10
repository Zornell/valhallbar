using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneJainitor : MonoBehaviour {

	private bool menuLoaded = false;
    private AudioSource _source;

	// Use this for initialization
	void Start () {
        SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
		SceneManager.LoadScene ("stage",LoadSceneMode.Additive);
	    _source = GetComponent<AudioSource>();
	}

    private void SceneManagerOnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "mainmenu")
        {
            var component = GameObject.Find("Start").GetComponent<Button>();
            component.onClick.AddListener(StartGame);

            var scrollText = GameObject.Find("ScrollText").GetComponent<Text>();
            scrollText.CrossFadeAlpha(0.0f, 2.0f, true);
        }
    }

    private void StartGame()
    {
        StartCoroutine(StartWithGame());
        _source.Play();
        var mainMenu = GameObject.Find("MainMenu");
        mainMenu.SetActive(false);
    }

    private IEnumerator StartWithGame()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("tavern", LoadSceneMode.Single);
    }

    // Update is called once per frame
	void Update () {
		if ((Input.anyKeyDown || Time.time>30f) && !menuLoaded ) {
			SceneManager.LoadScene ("mainmenu", LoadSceneMode.Additive);
			menuLoaded = true;
			Debug.Log ("loaded Mainmenu.");
		}
	}
}
