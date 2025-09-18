using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] Text _ammoText;
    [SerializeField] Text _highScoreText;
    [SerializeField] Text _currentScoreText;
    private int _score;

    [SerializeField] GameObject _gameOverUI;
    [SerializeField] Text _finalHighScoreText;
    [SerializeField] Text _finalScoreText;

    public bool isGameOver;
    public bool isGamePause;

    public List<Vector3> spawnPoints;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        Time.timeScale = 1.0f;
    }
    private void Start()
    {
        _highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        UpdateHighScore();
    } 

    public void UpdateAmmoRemaining(int ammo)
    {
        _ammoText.text = ammo.ToString();
    }
    public void UpdateHighScore()
    {
        if (_score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", _score);
            _highScoreText.text = _score.ToString();
        }
    }
    public void UpdateScore()
    {
        _score++;
        _currentScoreText.text = _score.ToString();
        UpdateHighScore();
    }

    public void Pause()
    {
        isGamePause = true;
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        isGamePause = false;
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        int index = Random.Range(0, 2);
        MusicManager.Instance.PlayMusic((MusicType)index);
    }
    public void BackHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetGameOverUI()
    {
        if (isGameOver)
        {
            _gameOverUI.SetActive(true);
            _finalHighScoreText.text = _highScoreText.text;
            _finalScoreText.text = _currentScoreText.text;
        }
    }
}
