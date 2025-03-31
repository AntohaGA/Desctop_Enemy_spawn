using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolEnemy))]
public class ChooserSpawn : MonoBehaviour
{
    private const int DelaySpawn = 1;

    [SerializeField] private List<EnemySpawner> _spawners;

    private PoolEnemy _enemyPool;

    private bool _isContinue = true;

    private IEnumerator _spawnCoroutine;

    private WaitForSeconds _delaySpawnEnemy;

    private void Awake()
    {
        _delaySpawnEnemy = new WaitForSeconds(DelaySpawn);
        _spawnCoroutine = SpawnDelay();
        _enemyPool = GetComponent<PoolEnemy>();
    }

    private void OnEnable()
    {
        StartCoroutine(_spawnCoroutine);
    }

    private void KillEnemy(Enemy enemy)
    {
        enemy.Killed -= KillEnemy;
        _enemyPool.KillEnemy(enemy);
    }

    private IEnumerator SpawnDelay()
    {
        Enemy enemy;

        while(_isContinue)
        {
            yield return _delaySpawnEnemy;

            enemy = _enemyPool.GetEnemy();
            _spawners[Random.Range(0, _spawners.Count)].SpawnEnemy(enemy);
            enemy.Killed += KillEnemy;
        }
    }
}