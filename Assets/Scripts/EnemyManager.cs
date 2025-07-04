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

    public static event Action<float, float> OnHeal;
    public float timeInterval;
    public float healAmount;
    public float globalHealBonus;
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
        ResetGlobalHealBonus();
        ResetGlobalSpeedBonus();
        StartCoroutine(IncreaseHealthAndSpeed());
        StartCoroutine(SpawnEnemies());
    }
    IEnumerator SpawnEnemies()
    {
        while (!_gameManager.isGameOver && !_gameManager.isGamePause)
        {
            yield return new WaitForSeconds(spawnRateEnemy);
            int index = Random.Range(0, _enemyPrefabs.Count);
            Enemy enemy = ObjectPooler.Instance.Getcomp(_enemyPrefabs[index]);
            enemy.Init();
            enemy.gameObject.SetActive(true);
        }
    }
    IEnumerator IncreaseHealthAndSpeed()
    {
        while (!_gameManager.isGameOver && !_gameManager.isGamePause)
        {
            yield return new WaitForSeconds(timeInterval);
            globalHealBonus += healAmount;
            globalSpeedBonus += speedAmount;
            OnHeal?.Invoke(healAmount, speedAmount);
            SFXManager.Instance.PlaySFX(SFXType.warning);
        }
    }
    public float GetGlobalHealBonus()
    {
        return globalHealBonus;
    }
    public void ResetGlobalHealBonus()
    {
        globalHealBonus = 0f;
    }
    public float GetGlobalSpeedBonus()
    {
        return globalSpeedBonus;
    }
    public void ResetGlobalSpeedBonus()
    {
        globalHealBonus = 0f;
    }
}
