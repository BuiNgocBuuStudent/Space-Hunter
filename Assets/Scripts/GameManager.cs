using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public Text ammoText;

    [SerializeField] private Text highScoreText;
    [SerializeField] private Text currentScoreText;
    private int score;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Text finalHighScoreText;
    [SerializeField] private Text finalScoreText;

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
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        UpdateHighScore();
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
    public void UpdateBulletRemaining(int bullet)
    {
        ammoText.text = bullet.ToString();
    }
    public void UpdateHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = score.ToString();
        }
    }
    public void UpdateScore()
    {
        score++;
        currentScoreText.text = score.ToString();
        UpdateHighScore();
    }

    public void Pause()
    {
        isGamePause = true;
    }
    public void Resume()
    {
        isGamePause = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetGameOverUI()
    {
        if (isGameOver)
        {
            gameOverUI.SetActive(true);
            finalScoreText.text = currentScoreText.text;
            finalHighScoreText.text = highScoreText.text;
        }
    }
}
