using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOutOfBounds : MonoBehaviour
{
    private GameManager _gameManager;
    private float _limitPosX = -21.0f;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < _limitPosX)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Game Over!!");
                _gameManager.isGameOver = true;
                Time.timeScale = 0f;
                _gameManager.SetGameOverUI();
            }
            gameObject.SetActive(false);
        }
        else if (transform.position.x > -_limitPosX + 2)
        {
            gameObject.SetActive(false);
        }
    }
}
