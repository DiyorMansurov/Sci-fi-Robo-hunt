using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float _spawnDelay;
    
    [SerializeField] private EnemyPool _pool;

    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;

    [SerializeField] private float _minimumSpeed = 5f;
    [SerializeField] private float _maximumSpeed = 10f;

    [SerializeField] private int _minAmountSpawning = 1;
    [SerializeField] private int _maxAmountSpawning = 10;
    private bool IsEnded = false;



    private float _timer;

    public static SpawnManager Instance;


    private void Awake() {
        Instance = this;
    }
    private void Update() {
        SpawmEnemyCountdown();
    }

    public void IsEndedActivate()
    {
        IsEnded = true;
    }

    private void SpawmEnemyCountdown()
    {
        if (IsEnded) return;

        _timer += Time.deltaTime;

        if (_timer >= _spawnDelay)
        {
            _timer = 0f;

            for (int i = 0; i < RandomSpawningAmout(); i++)
            {
                SpawnEnemy();
            }
        }
    }

    private float RandomSpawningAmout()
    {
        return Random.Range(_minAmountSpawning, _maxAmountSpawning);
    }

    public float RandomSpeed()
    {
        return Random.Range(_minimumSpeed, _maximumSpeed);
    }
    private void SpawnEnemy()
    {
        Enemy enemy = _pool.GetEnemy();
        enemy.Init(_startPoint.position, _pool, _endPoint.position);
    }

}
