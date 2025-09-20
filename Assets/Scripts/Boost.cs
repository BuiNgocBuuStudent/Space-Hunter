using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField] List<Vector3> _spawnPoints;
    [SerializeField] float _moveSpeed, _lifeTime;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void Init()
    {
        _spawnPoints = GameManager.Instance.spawnPoints;
        transform.position = GetRanDomPos();
    }
    private void FixedUpdate()
    {
        _rb.velocity = this.transform.right * -_moveSpeed;
        if (GameManager.Instance.isGameOver || GameManager.Instance.isGamePause)
            _rb.velocity = Vector2.zero;
    }
    private Vector3 GetRanDomPos()
    {
        int index = Random.Range(0, _spawnPoints.Count);
        return _spawnPoints[index];
    }
}
