using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyManager _enemyManager;
    private GameManager _gameManager;
    [Header("-------Reference Object-----")]
    [SerializeField] List<Vector3> _spawnPoints;
    [SerializeField] Bullet _bullet;
    [SerializeField] Move _move;
    [SerializeField] HealthBar _healthBar;
    [SerializeField] ParticleSystem _explosionParticle;

    [Header("-------Normal Type------")]
    [SerializeField] int _randomMaxHealth;
    [SerializeField] int _randomMinHealth;
    [SerializeField] int _startHealth;
    [SerializeField] int _currentHealth;

    private void Awake()
    {
        _enemyManager = EnemyManager.Instance;
        _move = GetComponent<Move>();
        _gameManager = GameManager.Instance;
    }
    public void Init()
    {
        _randomMinHealth += _enemyManager.GetGlobalHealBonus();
        _randomMaxHealth += _enemyManager.GetGlobalHealBonus();
        _move.speed += _enemyManager.GetGlobalSpeedBonus();

        _startHealth = Random.Range(_randomMinHealth, _randomMaxHealth);
        _currentHealth = _startHealth;
        _healthBar.SetMaxHealth(_startHealth);

        _spawnPoints = _gameManager.spawnPoints;
        transform.position = RandomPos();
    }
   
    private Vector3 RandomPos()
    {
        int index = Random.Range(0, _spawnPoints.Count);
        return _spawnPoints[index];

    }
    void Update()
    {
        if (_currentHealth <= 0)
        {
            _gameManager.UpdateScore();
            SFXManager.Instance.PlaySFX(SFXType.enemyDie);
            Instantiate(_explosionParticle, transform.position, _explosionParticle.transform.rotation);
            Destroy(this.gameObject);
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
