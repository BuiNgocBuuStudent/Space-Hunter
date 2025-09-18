using System.Collections;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private GameManager _gameManager;

    private float scrollSpeed;
    private float speedIncrement;
    private float timeInterval;
    private float _offset;
    private Material _mat;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        scrollSpeed = 0.8f;
        speedIncrement = 1.0f;
        timeInterval = 90.0f;
        _mat = GetComponent<Renderer>().material;
        StartCoroutine(increaseScrollSpeed());
    }


    void Update()
    {
        if (!_gameManager.isGameOver && !_gameManager.isGamePause)
        {
            _offset += (Time.deltaTime * scrollSpeed) / 10;
            _mat.SetTextureOffset("_MainTex", new Vector2(_offset, 0));
        }

    }
    IEnumerator increaseScrollSpeed()
    {
        while (!_gameManager.isGameOver && !_gameManager.isGamePause)
        {
            yield return new WaitForSeconds(timeInterval);
            scrollSpeed += speedIncrement;

        }

    }
}