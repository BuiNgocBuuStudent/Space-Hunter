using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class DetectCollisions : MonoBehaviour
{
    private BoostManager boostManager;
    // Start is called before the first frame update
    void Start()
    {
        boostManager = GameObject.Find("Boost Manager").GetComponent<BoostManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Game Over!!!");
            Destroy(other.gameObject);

            GameManager.Instance.isGameOver = true;
            GameManager.Instance.SetGameOverLogic();
        }
        if (other.gameObject.CompareTag("Boost"))
        {
            GameManager.Instance.isGamePause = true;
            boostManager.showBoostPopup();
            Destroy(other.gameObject);
        }
    }
}
