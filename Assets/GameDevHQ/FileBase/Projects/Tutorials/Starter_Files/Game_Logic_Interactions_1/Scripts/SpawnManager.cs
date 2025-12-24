using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float _spawnDelay;
    
    [SerializeField] private EnemyPool _pool;

    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;

    [SerializeField] private float _minimumSpeed = 3.5f;
    [SerializeField] private float _maximumSpeed = 7f;

    private float _timer;

    public static SpawnManager Instance;


    private void Awake() {
        Instance = this;
    }
    private void Update() {
        _timer += Time.deltaTime;

        if (_timer >= _spawnDelay)
        {
            _timer = 0f;
            SpawnEnemy();
        }
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
