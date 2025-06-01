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
                    setGameMode(2.0f, 30.0f, 10.0f, 1.0f, 1.5f, 100.0f);
                    break;
                case GameMode.normal:
                    setGameMode(1.8f, 31.0f, 10.0f, 1.0f, 1.5f, 90.0f);
                    break;
                case GameMode.hard:
                    setGameMode(1.6f, 32.0f, 10.0f, 1.0f, 1.3f, 80.0f);
                    break;
            }
        }
    }
    private void setGameMode(float spawnRateEnemy, float spawnRatePowerup, float startTimeSpawnPowerup, float healAmount, float speedAmount, float timeInterval)
    {
        GameManager.Instance.spawnRateEnemy = spawnRateEnemy;
        GameManager.Instance.spawnRatePowerup = spawnRatePowerup;
        GameManager.Instance.startTimeSpawnPowerup = startTimeSpawnPowerup;

        EnemyController.Instance.healAmount = healAmount;
        EnemyController.Instance.speedAmount = speedAmount;
        EnemyController.Instance.timeInterval = timeInterval;
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
        //Application.Quit();
    }
}