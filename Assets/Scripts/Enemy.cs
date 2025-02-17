using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Vector3> spawnPoints;

    public int health;
    public int income;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameManager.Instance.spawnPoints;

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
    public void IsAttacked(int damage)
    {
        health -= damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            IsAttacked(1);
            other.gameObject.SetActive(false);
            
        }

        if (health == 0)
        {
            GameManager.Instance.UpdateScore();
            GameManager.Instance.UpdateCoin(income);
            Destroy(gameObject);
        }
    }
}
