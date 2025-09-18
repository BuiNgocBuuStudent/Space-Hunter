using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;
    public static EnemyManager Instance => _instance;

    [SerializeField] List<Enemy> _enemyPrefabs;

    private GameManager _gameManager;

    public float timeInterval;
    public int healAmount;
    public int globalHealBonus;
    public float speedAmount;
    public float globalSpeedBonus;
    public float spawnRateEnemy;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;

        StartCoroutine(IncreaseHealAndSpeed());
        StartCoroutine(SpawnEnemies());
    }
    IEnumerator SpawnEnemies()
    {
        while (!_gameManager.isGameOver && !_gameManager.isGamePause)
        {
            yield return new WaitForSeconds(spawnRateEnemy);
            int index = Random.Range(0, _enemyPrefabs.Count);
            /*
             *Chấp nhận không sử dụng Object Pooling vì
             *gặp vấn đề trong logic tăng máu của enemy sau 
             *một khoảng thời gian
             */
            Enemy enemy = Instantiate(_enemyPrefabs[index], this.transform.position, Quaternion.identity);
            enemy.Init();
        }
    }
    IEnumerator IncreaseHealAndSpeed()
    {
        while (!_gameManager.isGameOver && !_gameManager.isGamePause)
        {
            yield return new WaitForSeconds(timeInterval);
            globalHealBonus += healAmount;
            globalSpeedBonus += speedAmount;
            SFXManager.Instance.PlaySFX(SFXType.warning);
        }
    }
    public int GetGlobalHealBonus()
    {
        return globalHealBonus;
    }
    public float GetGlobalSpeedBonus()
    {
        return globalSpeedBonus;
    }
}
