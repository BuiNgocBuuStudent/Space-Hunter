using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Vector3> spawnPoints;

    public Bullet bullet;

    public int maxHealth;
    public int currentHealth;
    public int income;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spawnPoints = GameManager.Instance.spawnPoints;
        transform.position = RandomPos();
    }

    private Vector3 RandomPos()
    {
        int index = Random.Range(0, spawnPoints.Count);
        return spawnPoints[index];
        
    }
    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            GameManager.Instance.UpdateScore();
            GameManager.Instance.UpdateCoin(income);
            Destroy(gameObject);
        }
    }
    public void IsAttacked(int damage)
    {
        Debug.Log("attack");
        currentHealth -= damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            IsAttacked(bullet.damage);
            other.gameObject.SetActive(false);
        }
    }
}
