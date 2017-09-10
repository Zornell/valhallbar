using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioClip AudioLoop;
	public AudioClip AudioIntro;

	private AudioSource _as;


	// Use this for initialization
	void Start () {
		_as = GetComponent<AudioSource> ();
		_as.clip = AudioIntro;
		_as.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_as.clip == AudioIntro && _as.isPlaying == false) {
			_as.clip = AudioLoop;
			_as.loop = true;
			_as.Play ();
		}		
	}
}
