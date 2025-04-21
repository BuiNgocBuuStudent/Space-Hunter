using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public float speed;

    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isGameOver && !GameManager.Instance.isGamePause)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

        }
    }
}
