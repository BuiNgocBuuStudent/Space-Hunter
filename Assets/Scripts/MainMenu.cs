using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private enum GameMode { easy, normal, hard }
    private GameMode selectedMode;


    public void setEasyMode()
    {
        selectedMode = GameMode.easy;
    }
    public void setNormalMode()
    {
        selectedMode = GameMode.normal;
    }
    public void setHardMode()
    {
        selectedMode = GameMode.hard;
    }
    public void PlayGame()
    {
        SceneManager.sceneLoaded += OnPlaySceneLoad;
        SceneManager.LoadScene("PlayGame");
    }

    private void OnPlaySceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "PlayGame")
        {
            switch (selectedMode)
            {
                case GameMode.easy:
                    setGameMode(2.0f, 30.0f, 10.0f, 1.0f, 1.0f, 100.0f);
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
}