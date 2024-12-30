using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Vector3> spawnPoints;
    private GameManager gameManager;

    public int health;
    public int income;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spawnPoints = gameManager.spawnPoints;

        transform.position = RanDomPos();
    }

    private Vector3 RanDomPos()
    {
        int index = Random.Range(0, spawnPoints.Count);
        return spawnPoints[index];
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void IsAttacked()
    {
        health--;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            IsAttacked();
            other.gameObject.SetActive(false);
            
        }

        if (health == 0)
        {
            gameManager.UpdateScore();
            gameManager.UpdateCoin(income);
            Destroy(gameObject);
        }
    }
}
