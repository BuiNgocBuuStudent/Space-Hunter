using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private enum GameMode { easy, normal, hard }
    private GameMode selectedMode;

    public Text currentModeText;

    private void Awake()
    {
        MusicManager.Instance.PlayMusic(MusicType.menuTheme);
    }
    private void Start()
    {
        setModeUI();
    }
    public void setEasyMode()
    {
        selectedMode = GameMode.easy;
        setModeUI();
    }
    public void setNormalMode()
    {
        selectedMode = GameMode.normal;
        setModeUI();
    }
    public void setHardMode()
    {
        selectedMode = GameMode.hard;
        setModeUI();
    }
    public void PlayGame()
    {
        SceneManager.sceneLoaded += OnPlaySceneLoad;
        SceneManager.LoadScene("PlayGame");
        int index = Random.Range(0, 2);
        MusicManager.Instance.PlayMusic((MusicType)index);
    }

    private void OnPlaySceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "PlayGame")
        {
            switch (selectedMode)
            {
                case GameMode.easy:
                    setGameMode(spawnRateEnemy: 2.0f, spawnRateBoost: 30.0f, startTimeSpawnBoost: 10.0f, healAmount: 1.0f, speedAmount: 1.5f, timeInterval: 100.0f);
                    break;
                case GameMode.normal:
                    setGameMode(spawnRateEnemy: 1.8f, spawnRateBoost: 31.0f, startTimeSpawnBoost: 10.0f, healAmount: 1.0f, speedAmount: 1.5f, timeInterval: 90.0f);
                    break;
                case GameMode.hard:
                    setGameMode(spawnRateEnemy: 1.6f, spawnRateBoost: 32.0f, startTimeSpawnBoost: 10.0f, healAmount: 1.0f, speedAmount: 1.3f, timeInterval: 80.0f);
                    break;
            }
        }
    }
    private void setGameMode(float spawnRateEnemy, float spawnRateBoost, float startTimeSpawnBoost, float healAmount, float speedAmount, float timeInterval)
    {
        EnemyManager.Instance.spawnRateEnemy = spawnRateEnemy;
        BoostManager.Instance.spawnRateBoost = spawnRateBoost;
        BoostManager.Instance.startTimeSpawnBoost = startTimeSpawnBoost;

        EnemyManager.Instance.healAmount = healAmount;
        EnemyManager.Instance.speedAmount = speedAmount;
        EnemyManager.Instance.timeInterval = timeInterval;
    }

    public void setModeUI()
    {

        if (selectedMode == GameMode.easy)
        {
            currentModeText.color = Color.green;
        }
        else if (selectedMode == GameMode.normal)
        {
            currentModeText.color = Color.yellow;
        }
        else if (selectedMode == GameMode.hard)
        {
            currentModeText.color = Color.red;
        }
        currentModeText.text = "Mode: " + selectedMode;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}