using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{

    private float limitPosX = -21.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < limitPosX)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Game Over!!");
                GameManager.Instance.isGameOver = true;
                GameManager.Instance.SetGameOverLogic();
            }
            Destroy(gameObject);
        }
        else if(transform.position.x > -limitPosX + 2)
        {
            gameObject.SetActive(false);
            
        }
    }
}
