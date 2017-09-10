using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameManager GameManager;
    public Text Text;
    private int _score;

    public void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        GameManager.EnemyKilled += GameManagerOnEnemyKilled;
    }

    private void GameManagerOnEnemyKilled(object sender, EnemyKilledEventArgs args)
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        _score++;
        Text.text = string.Format("{0} Foes vanquished!", _score);
    }
}