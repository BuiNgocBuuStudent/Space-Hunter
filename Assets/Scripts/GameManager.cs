using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text bulletRemainingText;

    public Text scoreText;
    private int score;

    public Text coinText;
    private int coin;
    public bool isGameOver;

    public List<Vector3> spawnPoints;

    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private GameObject powerupPrefabs;

    public float spawnRateEnemy = 1.5f;
    public float spawnRatePowerup = 10.0f;
    public float startTimeSpawnPowerup = 10.0f;


    private void Start()
    {
        isGameOver = false;

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        Debug.Log("Start Game");
        StartCoroutine(SpawnZombie());
        InvokeRepeating("SpawnPowerup", startTimeSpawnPowerup, spawnRatePowerup);

    }


    IEnumerator SpawnZombie()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRateEnemy);
            int index = Random.Range(0, enemyPrefabs.Count);
            Instantiate(enemyPrefabs[index]);
        }

    }

    private void SpawnPowerup()
    {
        if (!isGameOver)
        {
            Instantiate(powerupPrefabs);
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
