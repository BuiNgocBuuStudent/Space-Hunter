using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyManager _enemyManager;
    private GameManager _gameManager;
    private Rigidbody _rb;

    [SerializeField] List<Vector3> _spawnPoints;
    [SerializeField] Bullet _bullet;
    [SerializeField] HealthBar _healthBar;
    [SerializeField] ParticleSystem _explosionParticle;

    [SerializeField] int _randomMinHealth, _randomMaxHealth, _currentHealth, _startHealth;
    [SerializeField] float _moveSpeed, _lifeTime;
    private void Awake()
    {
        _enemyManager = EnemyManager.Instance;
        _rb = this.GetComponent<Rigidbody>();
        _gameManager = GameManager.Instance;
        StartCoroutine(CheckLifeTime());
    }
    public void Init()
    {
        _randomMinHealth += _enemyManager.GetGlobalHealBonus();
        _randomMaxHealth += _enemyManager.GetGlobalHealBonus();
        _moveSpeed += _enemyManager.GetGlobalSpeedBonus();

        _startHealth = Random.Range(_randomMinHealth, _randomMaxHealth);
        _currentHealth = _startHealth;
        _healthBar.SetMaxHealth(_startHealth);

        _spawnPoints = _gameManager.spawnPoints;
        transform.position = GetRandomPos();
    }

    private Vector3 GetRandomPos()
    {
        int index = Random.Range(0, _spawnPoints.Count);
        return _spawnPoints[index];

    }
    void Update()
    {
        CheckEnemyDied();
        _lifeTime -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        _rb.velocity = this.transform.right * -_moveSpeed * Time.deltaTime;
    }
    private void CheckEnemyDied()
    {
        if (_currentHealth <= 0)
        {
            _gameManager.UpdateScore();
            SFXManager.Instance.PlaySFX(SFXType.enemyDie);
            Instantiate(_explosionParticle, transform.position, _explosionParticle.transform.rotation);
            Destroy(this.gameObject);
        }
    }
    private void CheckIsAttacked(int damage)
    {
        _currentHealth -= damage;
        _healthBar.SetHeath(_currentHealth);
    }
    IEnumerator CheckLifeTime()
    {
        while (!_gameManager.isGameOver && !_gameManager.isGamePause)
        {
            yield return new WaitForSeconds(_lifeTime);
            _gameManager.isGameOver = true;
            Time.timeScale = 0f;
            _gameManager.SetGameOverUI();
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            CheckIsAttacked(_bullet.damage);
            SFXManager.Instance.PlaySFX(SFXType.hit);
            other.gameObject.SetActive(false);
        }
    }
}
