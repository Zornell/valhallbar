using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;
	public GameObject VikingPrefab;
	public GameObject BloodExplosion;

	public TextAsset LevelData;
    [Range(-2f, 3f)]
    public float SpawnHeightOffset = 1.5f;

    public int Lanes = 5;
    public int LaneOffset = 2;
    public int Speed = 4;
    
    private Viking _viking;
    private readonly List<Enemy> _enemies = new List<Enemy>();

	// Use this for initialization
	void Start ()
    {
        Debug.Assert(EnemyPrefabs.Length == 5);

		_viking = Instantiate(VikingPrefab).GetComponent<Viking>();
		_viking.laneOffset = LaneOffset;


		var lines = LevelData.text.Split(new [] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		foreach (var line in lines)
		{
			var strings = line.Split(',');
			var time = int.Parse(strings[0]);
			var lane = int.Parse(strings[1]);
			var enemyType = int.Parse(strings[2]);

		    Debug.Assert(enemyType < 6);

		    var enemyObject = Instantiate (EnemyPrefabs[enemyType]);
		    var newObj = enemyObject.GetComponent<Enemy>();

			newObj.lane = -(Lanes/2) + lane;
			newObj.laneOffset = LaneOffset;
			newObj.height = -time / 1000f * Speed + SpawnHeightOffset;
            
			_enemies.Add (newObj);
		}
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Move
		if (Input.GetKeyDown (KeyCode.LeftArrow))
        {
			_viking.Move (-1);
		}
        else if (Input.GetKeyDown (KeyCode.RightArrow))
        {
			_viking.Move (1);
		}


        var attackTriggered = Input.GetKeyDown(KeyCode.Space);

        for (int i = 0; i < _enemies.Count; ++i)
        {
            var enemy = _enemies[i];
            enemy.Move(Speed * Time.deltaTime);

            if (EnemyPassedBy(enemy))
            {
                RemoveEnemy(enemy, ref i);
            }
		}
	}

    private void RemoveEnemy(Enemy enemy, ref int i)
    {
		//blood explosion
		Instantiate(BloodExplosion, enemy.transform.position, enemy.transform.rotation);

		Destroy(enemy.gameObject);
        _enemies.Remove(enemy);
        --i;
    }

    private bool EnemyPassedBy(Enemy enemy)
    {
        return enemy.height > _viking.Height + 2;
    }
}
