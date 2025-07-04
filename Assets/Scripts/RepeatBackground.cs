using System.Collections;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private GameManager gameManager;

    public float scrollSpeed;
    public float speedIncrement;
    public float timeInterval;
    private float _offset;
    private Material _mat;

    private void Start()
    {
        gameManager = GameManager.Instance;
        scrollSpeed = 1.0f;
        speedIncrement = 1.0f;
        timeInterval = 90.0f;
        _mat = GetComponent<Renderer>().material;
        StartCoroutine(increaseScrollSpeed());
    }


    void Update()
    {
        if (!gameManager.isGameOver && !gameManager.isGamePause)
        {
            _offset += (Time.deltaTime * scrollSpeed) / 10;
            _mat.SetTextureOffset("_MainTex", new Vector2(_offset, 0));
        }

    }
    IEnumerator increaseScrollSpeed()
    {
        while (!gameManager.isGameOver)
        {
            while (gameManager.isGamePause)
            {
                yield return null;
            }

            float elapsedTime = 0f;
            while (elapsedTime < timeInterval)
            {
                if (gameManager.isGamePause || GameManager.Instance.isGameOver)
                {
                    yield return null;
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                }
                yield return null;
            }
            if (!gameManager.isGameOver)
            {
                scrollSpeed += speedIncrement;
            }

        }

    }
}