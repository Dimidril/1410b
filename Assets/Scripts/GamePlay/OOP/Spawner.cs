using UnityEngine;
using System.Collections;

public class Spawner : NonCombat
{
    #region Fields

    #region Serialized Fields

    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Point _pointToAttack;
    [Range(2, 100)]
    [SerializeField] private float _spawnTimer = 4f;

    #endregion

    #region Private Fields

    private bool isAttacking;
    private IEnumerator _spawnCoroutine;

    #endregion

    #endregion

    #region Methods

    #region Unity Methods

    private void Awake()
    {
        _spawnCoroutine = SpawnCoroutine();
        LevelManager.instance.changePoint += StartSpawn;
        LevelManager.instance.RetreatTime += StopSpawn;
        LevelManager.instance.gameOver += StopSpawn;
        StartSpawn(LevelManager.instance.ActivePoint);
    }

    #endregion

    #region Private Methods
    private void StopSpawn()
    {
        StopCoroutine(_spawnCoroutine);
    }
    private void StopSpawn(Point next)
    {
        StopSpawn();
    }

    private void StartSpawn(Point newPoint)
    {
        if (isAttacking)
        {
            isAttacking = false;
            StopCoroutine(_spawnCoroutine);
        }
        if (newPoint == _pointToAttack)
        {
            isAttacking = true;
            StartCoroutine(_spawnCoroutine);
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            Enemy _currentEnemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_spawnTimer + Random.Range(-1f, 5f));
        }
    }

    #endregion

    #endregion
}