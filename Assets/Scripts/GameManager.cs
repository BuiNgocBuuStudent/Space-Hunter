using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text bulletRemainingText;

    public Text scoreText;
    private int score;

    public Text coinText;
    private int coin;

    public bool isGameOver;
    public bool isGamePause;

    public List<Vector3> spawnPoints;

    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private GameObject powerupPrefab;

    public float spawnRateEnemy;
    public float spawnRatePowerup;
    public float startTimeSpawnPowerup;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        
        StartGame();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        Debug.Log("Start Game");
        StartCoroutine(SpawnEnemies());
        InvokeRepeating("SpawnPowerups", startTimeSpawnPowerup, spawnRatePowerup);
    }


    IEnumerator SpawnEnemies()
    {
        while (!isGameOver)
        {
            while (isGamePause)
            {
                yield return null; // Dừng lại và chờ đến khi resume
            }

            yield return new WaitForSeconds(spawnRateEnemy);

            if (isGamePause) continue; // Kiểm tra lại nếu pause sau khi delay

            int index = Random.Range(0, enemyPrefabs.Count);
            Instantiate(enemyPrefabs[index]);
        }
    }

    private void SpawnPowerups()
    {
        if (!isGameOver && !isGamePause)
        {
            Instantiate(powerupPrefab);
        }

    }
    public void UpdateBulletRemaining(int bulletToAdd)
    {
        bulletRemainingText.text = "Bullet: " + bulletToAdd;
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + ++score;
    }

    public void UpdateCoin(int coinToAdd)
    {
        coin += coinToAdd;
        coinText.text = "Coin: " + coin;
    }

}
