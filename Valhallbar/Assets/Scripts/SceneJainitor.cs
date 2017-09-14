using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneJainitor : MonoBehaviour {

	private bool menuLoaded = false;
    private AudioSource _source;

	// Use this for initialization
	void Start ()
    {
        SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
		SceneManager.LoadScene ("stage",LoadSceneMode.Additive);
	    _source = GetComponent<AudioSource>();
	}

    private void SceneManagerOnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name != "mainmenu") return;

        var component = GameObject.Find("Start").GetComponent<Button>();
        component.onClick.AddListener(StartGame);

        var scrollText = GameObject.Find("ScrollText").GetComponent<Text>();
        scrollText.CrossFadeAlpha(0.0f, 2.0f, true);
        
        var mainMenu = GameObject.Find("MenuContent").GetComponent<RectTransform>();
        var endPosition = mainMenu.position;
        mainMenu.localPosition = new Vector3(350, 1000, 0);
        StartCoroutine(SlideInMenu(mainMenu, endPosition));
    }

    private IEnumerator SlideInMenu(RectTransform menu, Vector3 endPosition)
    {
        float usedTime = 0;
        var startPosition = menu.position;
        while (usedTime < 1)
        {
            usedTime += Time.deltaTime;
            menu.position = Vector3.Lerp(startPosition, endPosition, usedTime / 1);

            yield return new WaitForEndOfFrame();
        }
    }

    private void StartGame()
    {
        StartCoroutine(StartWithGame());
        _source.Play();
        var mainMenu = GameObject.Find("MainMenu");
        var karaokee = GameObject.Find("Karaokee");

        karaokee.SetActive(false);
        mainMenu.SetActive(false);
    }

    private IEnumerator StartWithGame()
    {
        yield return new WaitForSeconds(8f);

        SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;

        SceneManager.UnloadSceneAsync("mainmenu");
        SceneManager.UnloadSceneAsync("stage");
        SceneManager.UnloadSceneAsync("main");



        SceneManager.LoadScene("tavern", LoadSceneMode.Single);
    }

    // Update is called once per frame
	void Update ()
    {
		if ((Input.anyKeyDown || Time.time > 45f) && !menuLoaded )
        {
			SceneManager.LoadScene ("mainmenu", LoadSceneMode.Additive);
			menuLoaded = true;
		}
	}
}
