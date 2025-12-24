using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _poolSize = 10;

    private Queue<Enemy> _pool = new Queue<Enemy>();
    void Awake()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            Enemy enemy = Instantiate(_enemyPrefab, transform);
            enemy.gameObject.SetActive(false);
            _pool.Enqueue(enemy);
        }
    }


    public Enemy GetEnemy()
    {
        if (_pool.Count > 0)
        {
            return _pool.Dequeue();
        }
        else
        {
            Enemy enemy = Instantiate(_enemyPrefab, transform);
            enemy.gameObject.SetActive(false);
            return enemy;
        }
    }

    public void ReturnEnemyToPool(Enemy enemy)
    {
        _pool.Enqueue(enemy);
        
    }
}
