using System;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(AudioSource))]
public class HitSoundPlayer : MonoBehaviour
{
    public AudioSource HitSoundSource;
    public AudioClip[] HitSounds;
    private GameManager _gameManager;
    

    // Use this for initialization
    void Start()
    {
        _gameManager = GetComponent<GameManager>();
        _gameManager.EnemyKilled += GameManagerOnEnemyKilled;
    }

    private void GameManagerOnEnemyKilled(object sender, EventArgs eventArgs)
    {
        var soundIdx = Mathf.FloorToInt(UnityEngine.Random.Range(0, HitSounds.Length - 0.1f));
        HitSoundSource.PlayOneShot(HitSounds[soundIdx]);
    }
}