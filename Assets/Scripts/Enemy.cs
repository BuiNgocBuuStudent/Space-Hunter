using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyManager _enemyManager; 
    private GameManager _gameManager;
    [SerializeField] List<Vector3> _spawnPoints;

    [SerializeField] Bullet _bullet;
    [SerializeField] Move _move;
    [SerializeField] HealthBar _healthBar;
    [SerializeField] float _maxHealth;
    [SerializeField] float _currentHealth;

    [SerializeField] ParticleSystem _explosionParticle;
    private void Awake()
    {
        _enemyManager = EnemyManager.Instance;
        _move = GetComponent<Move>();
        _move.speed += _enemyManager.GetGlobalSpeedBonus();
        _maxHealth += _enemyManager.GetGlobalHealBonus();
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    public void Init()
    {
        _gameManager = GameManager.Instance;
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
        _spawnPoints = _gameManager.spawnPoints;
        transform.position = RandomPos();
    }
    private void OnEnable()
    {
        EnemyManager.OnHeal += IncHealthAndSpeed;
    }
    private void OnDisable()
    {
        EnemyManager.OnHeal -= IncHealthAndSpeed;
    }

    private void IncHealthAndSpeed(float healAmount, float speedAmount)
    {
        _maxHealth += healAmount;
        _healthBar.SetMaxHealth(_maxHealth);
        _move.speed += speedAmount;
    }

    private Vector3 RandomPos()
    {
        int index = Random.Range(0, _spawnPoints.Count);
        return _spawnPoints[index];

    }
    // Update is called once per frame
    void Update()
    {
        if (_currentHealth <= 0)
        {
            _gameManager.UpdateScore();
            SFXManager.Instance.PlaySFX(SFXType.enemyDie);
            Instantiate(_explosionParticle, transform.position, _explosionParticle.transform.rotation);
            gameObject.SetActive(false);
        }
    }
    public void IsAttacked(int damage)
    {
        _currentHealth -= damage;
        _healthBar.SetHeath(_currentHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            IsAttacked(_bullet.damage);
            SFXManager.Instance.PlaySFX(SFXType.hit);
            other.gameObject.SetActive(false);
        }
    }

}
