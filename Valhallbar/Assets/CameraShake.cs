using System;
using EZCameraShake;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class CameraShake : MonoBehaviour
{
    private GameManager _gameManager;

    public float Magnitude = 3;
    public float Roughness = 3;
    public float FadeInTime = 0.2f;
    public float FadeOutTime = 0.2f;

    // Use this for initialization
	void Start ()
	{
	    _gameManager = GetComponent<GameManager>();
        _gameManager.EnemyKilled += GameManagerOnEnemyKilled;
	}

    private void GameManagerOnEnemyKilled(object sender, EventArgs eventArgs)
    {
        CameraShaker.Instance.ShakeOnce(Magnitude, Roughness, FadeInTime, FadeOutTime);
    }
}