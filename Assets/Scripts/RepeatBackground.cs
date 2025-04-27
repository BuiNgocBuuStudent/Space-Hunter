using System.Collections;
using System.Xml;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{

    public float scrollSpeed;
    public float speedIncrement;
    public float timeInterval;
    private float offset;
    private Material mat;


    private void Start()
    {
        scrollSpeed = 0.5f;
        speedIncrement = 0.5f;
        timeInterval = 60.0f;
        mat = GetComponent<Renderer>().material;
        StartCoroutine(increaseScrollSpeed());
    }


    void Update()
    {
        if (!GameManager.Instance.isGameOver && !GameManager.Instance.isGamePause)
        {
            offset += (Time.deltaTime * scrollSpeed) / 10;
            mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }

    }
    IEnumerator increaseScrollSpeed()
    {
        while (!GameManager.Instance.isGameOver)
        {
            while (GameManager.Instance.isGamePause)
            {
                yield return null;
            }

            float elapsedTime = 0f;
            while (elapsedTime < timeInterval)
            {
                if (GameManager.Instance.isGamePause || GameManager.Instance.isGameOver)
                {
                    yield return null;
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                }
                yield return null;
            }
            if (!GameManager.Instance.isGameOver)
            {
                scrollSpeed += speedIncrement;
                Debug.Log("Scroll speed: " + scrollSpeed);
            }

        }

    }
}