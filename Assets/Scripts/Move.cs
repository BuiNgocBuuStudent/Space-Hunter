using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private GameManager playerCtrl;

    [SerializeField] private float speed;

    private void Start()
    {
        playerCtrl = GameObject.FindObjectOfType<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!playerCtrl.isGameOver)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

        }
    }
}
