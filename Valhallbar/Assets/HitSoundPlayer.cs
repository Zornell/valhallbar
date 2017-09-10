using System;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(AudioSource))]
public class HitSoundPlayer : MonoBehaviour
{
    public AudioSource HitSoundSource;
	public AudioClip SwitchLaneSoundSource;
	public AudioClip AxeSoundSource;
	public AudioClip[] HitSounds;
    private GameManager _gameManager;
    

    // Use this for initialization
    void Start()
    {
        _gameManager = GetComponent<GameManager>();
        _gameManager.EnemyKilled += GameManagerOnEnemyKilled;
		_gameManager.HitMove += PlayAxeSound;
		_gameManager.SwitchLaneMove += PlaySwitchLane;
	}

    private void GameManagerOnEnemyKilled(object sender, EnemyKilledEventArgs eventArgs)
    {
        var soundIdx = Mathf.FloorToInt(UnityEngine.Random.Range(0, HitSounds.Length - 0.1f));
        
        HitSoundSource.PlayOneShot(HitSounds[soundIdx]);
        var t = (eventArgs.Lane + GameManager.Lanes/2f) / GameManager.Lanes;
        HitSoundSource.panStereo = Mathf.Lerp(-1f, 1f, t);
    }

	private void PlayAxeSound(object sender, EventArgs eventArgs)
	{
		//HitSoundSource.PlayOneShot(AxeSoundSource);
	}

	private void PlaySwitchLane(object sender, EventArgs eventArgs)
	{
		HitSoundSource.PlayOneShot(SwitchLaneSoundSource);
	}
}