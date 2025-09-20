using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    [SerializeField] float _moveSpeed;
    private Rigidbody _rb;
    private void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        _rb.velocity = this.transform.right * -_moveSpeed;
        if (GameManager.Instance.isGameOver || GameManager.Instance.isGamePause)
            _rb.velocity = Vector2.zero;
    }

}
