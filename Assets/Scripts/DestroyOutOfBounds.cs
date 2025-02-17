using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{

    private float limitPosX = -30.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < limitPosX)
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Game Over!!");
                GameManager.Instance.isGameOver = true;
            }
        }
        else if(transform.position.x > -limitPosX)
        {
            gameObject.SetActive(false);
            
        }
    }
}
