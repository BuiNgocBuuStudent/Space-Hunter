using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Vector3> spawnPoints;

    public Bullet bullet;

    public float maxHealth;
    public float currentHealth;
    public int income;
    private void Awake()
    {
        if(EnemyController.Instance != null)
        {
            maxHealth += EnemyController.Instance.GetGlobalHealBonus();
        }
        else
        {
            Debug.Log("EnemyController is null");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spawnPoints = GameManager.Instance.spawnPoints;
        transform.position = RandomPos();
    }
    private void OnEnable()
    {
        EnemyController.OnHeal += IncreaseHealth;
    }
    private void OnDisable()
    {
        EnemyController.OnHeal -= IncreaseHealth;
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
        currentHealth -= damage;
    }
    private void IncreaseHealth(float amount)
    {
        maxHealth += amount;
        Debug.Log("Max health increased to: " + maxHealth);
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
