using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Enemy;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class AutoBullet : MonoBehaviour
{
    [SerializeField]
    private float timeTrackTarget;

    [SerializeField]
    private float distanceToEnemy;

    private Rigidbody2D _rb;
    private float _bulletDirection;
    private float _bulletDamage;
    private float _bulletSpeed;
    private float _bulletLifeTime;
    private float _bulletLifeTimer;
    private GameObject _enemy;
    private bool _canMove;

    internal void SetProperties(
        float bulletDamage,
        float bulletSpeed,
        float bulletLifeTime,
        float bulletDirection = 1
    )
    {
        _bulletDamage = bulletDamage;
        _bulletSpeed = bulletSpeed;
        _bulletLifeTime = bulletLifeTime;
        _bulletDirection = bulletDirection;
    }

    void Reset()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _bulletLifeTimer = 0;
    }

    void Update()
    {
        _bulletLifeTimer += Time.deltaTime;
        if (_bulletLifeTimer > _bulletLifeTime)
        {
            Destroy(gameObject);
        }
        CanMove();
        if (_canMove)
        {
            Vector2 enemyPosition = _enemy.transform.position;
            Debug.Log(_enemy.transform.position);
            transform.position = Vector2.Lerp(
                transform.position,
                enemyPosition,
                _bulletSpeed * Time.deltaTime
            );
        }
    }

    private void CanMove()
    {
        StartCoroutine(TargetEnemy());
    }

    private IEnumerator TargetEnemy()
    {
        _enemy = GameObject.FindGameObjectWithTag("Enemy");
        yield return new WaitForSeconds(timeTrackTarget);
        if (
            _enemy != null
            && Vector2.Distance(transform.position, _enemy.transform.position) < distanceToEnemy
        )
        {
            _canMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BaseEnemy>())
        {
            // TODO: Animation
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToEnemy);
    }

    public float GetDoDamage() => _bulletDamage;
}
