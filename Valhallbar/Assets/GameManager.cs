using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public event EventHandler<EnemyKilledEventArgs> EnemyKilled;

    public GameObject[] EnemyPrefabs;
	public GameObject VikingPrefab;
	public GameObject BloodExplosion;
    public AudioSource AudioSource;
	public int PercentDamagePerHit;
	public Slider HealthSlider;
	public TextAsset LevelData;

    private float SpawnHeightOffset = 1.8f;
	//100% health
	private int PlayerHealth = 100;


	public static int Lanes = 5;
    public static int LaneOffset = 2;
    public int Speed = 4;


    private InputHandler _attackHandler;
    private InputHandler _moveHandler;
    private InputHandler _spinHandler;

    private Viking _viking;
    private readonly List<Enemy> _enemies = new List<Enemy>();
    private bool _attackTriggered;
    private bool _spinTriggered;


    void Start ()
    {
        Debug.Assert(EnemyPrefabs.Length == 5);

		_viking = Instantiate(VikingPrefab).GetComponent<Viking>();
		_viking.laneOffset = LaneOffset;

        _attackHandler = new InputHandler("Fire1", i => _attackTriggered = true);
        _spinHandler = new InputHandler("Fire2", i => _spinTriggered = true);
        _moveHandler = new InputHandler("Horizontal", _viking.Move);

		var r = new System.Random();

		var lines = LevelData.text.Split(new [] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach (var line in lines)
		{
			var strings = line.Split(',');
			var time = int.Parse(strings[0]);
			var lane = int.Parse(strings[1]);
			//var enemyType = int.Parse(strings[2]);
			var enemyType = r.Next(5);

		    Debug.Assert(enemyType < 6);

		    var enemyObject = Instantiate (EnemyPrefabs[enemyType]);
		    var newObj = enemyObject.GetComponent<Enemy>();

			newObj.Lane = -(Lanes/2) + lane;
			newObj.laneOffset = LaneOffset;
			newObj.height = -time / 1000f * Speed + SpawnHeightOffset;
		    newObj.time = time;
            
			_enemies.Add (newObj);
		}

        foreach (var grouping in _enemies.ToLookup(e => e.time).Where(grp => grp.Count() > 1).SelectMany(x => x))
        {
            grouping.height += 0.5f;
        }
	}

    void Update()
    {
        ResetState();

        foreach (var enemy in _enemies)
        {
            enemy.MoveTo(Speed * AudioSource.time);
        }

        if (!_enemies.Any()) return;

		int max_props = 5;
		if (_enemies.Count < 5)
			max_props = _enemies.Count;

		for (int i = 0; i < max_props; ++i) {
			var nextEnemy = _enemies[i];

			if (EnemyHitViking(nextEnemy))
			{
				RemoveEnemy(nextEnemy);

				PlayerHealth -= PercentDamagePerHit;
				HealthSlider.value = PlayerHealth;

				if (PlayerHealth <= 0)
				{
					//TODO: GameOver();
				}

				return;
			}

			if (EnemyPassedBy(nextEnemy))
			{
				RemoveEnemy(nextEnemy);
				return;
			}

			if (_attackTriggered && EnemyCanBeHit(nextEnemy))
			{
				RemoveEnemy(nextEnemy);
				return;
			}

			if ( i + 1 >= max_props) return;

			var secondEnemy = _enemies[i+1];
			if (_spinTriggered && EnemiesCanBeSpinHit(nextEnemy, secondEnemy))
			{
				RemoveEnemy(nextEnemy);
				RemoveEnemy(secondEnemy);
			}
		}

        
	}

    private void ResetState()
    {
        _attackTriggered = false;
        _spinTriggered = false;

        _moveHandler.Process();
        _attackHandler.Process();
        _spinHandler.Process();
    }

    private bool EnemiesCanBeSpinHit(Enemy enemy1, Enemy enemy2)
    {
        var col1 = enemy1.GetComponent<BoxCollider2D>();
        var col2 = enemy2.GetComponent<BoxCollider2D>();

        var left = _viking.SpinCollider[0];
        var right = _viking.SpinCollider[1];

        var spin1 = left.IsTouching(col1) || right.IsTouching(col1);
        var spin2 = left.IsTouching(col2) || right.IsTouching(col2);

        return spin1 && spin2;
    }

    private bool EnemyCanBeHit(Enemy enemy)
    {
        var enemyCollider = enemy.GetComponent<BoxCollider2D>();

        return _viking.AttackCollider.IsTouching(enemyCollider);
    }

    private bool EnemyHitViking(Enemy enemy)
    {
        var enemyCollider = enemy.GetComponent<BoxCollider2D>();

        return _viking.HitCollider.IsTouching(enemyCollider);
    }
    

    private void RemoveEnemy(Enemy enemy)
    {
		//blood explosion
		Instantiate(BloodExplosion, enemy.transform.position, enemy.transform.rotation);

		Destroy(enemy.gameObject);
        _enemies.Remove(enemy);

        OnEnemyKilled(enemy);
    }

    private bool EnemyPassedBy(Enemy enemy)
    {
        return enemy.currentHeight > _viking.Height + 2;
    }

    protected virtual void OnEnemyKilled(Enemy enemy)
    {
        var handler = EnemyKilled;
        if (handler != null) handler(this, new EnemyKilledEventArgs()
        {
            EnemyType = enemy.EnemyType,
            Lane = enemy.Lane
        });
    }

}