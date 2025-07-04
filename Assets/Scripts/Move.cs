using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private GameManager _gameManager;
    public float speed;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }
    // Update is called once per frame
    void Update()
    {
        if (!_gameManager.isGameOver && !_gameManager.isGamePause)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

        }
    }
}
