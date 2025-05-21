using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Vector3> spawnPoints;

    public Bullet bullet;
    public Move move;
    public float maxHealth;
    public float currentHealth;
    private void Awake()
    {
        move = GetComponent<Move>();
        move.speed += EnemyController.Instance.GetGlobalSpeedBonus();
        maxHealth += EnemyController.Instance.GetGlobalHealBonus();
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
        EnemyController.OnHeal += IncHealthAndSpeed;
    }
    private void OnDisable()
    {
        EnemyController.OnHeal -= IncHealthAndSpeed;
    }

    private void IncHealthAndSpeed(float healAmount, float speedAmount)
    {
        maxHealth += healAmount;
        move.speed += speedAmount;
        Debug.Log("Max health increased to: " + maxHealth + "and speed: " + move.speed);
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
            Destroy(gameObject);
        }
    }
    public void IsAttacked(int damage)
    {
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
