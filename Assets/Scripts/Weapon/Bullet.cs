using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
    // TODO: hay que añadir un script para que cuando se instancie se destruya tambien
    // [SerializeField]
    // private GameObject explodeEffect;

    private Rigidbody2D _rb;
    private float _bulletDirection;
    private float _bulletDamage;
    private float _bulletSpeed;
    private float _bulletLifeTime;
    private float _bulletLifeTimer;

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
        transform.localScale = new Vector2(
            transform.localScale.x * _bulletDirection,
            transform.localScale.y
        );
    }

    void Update()
    {
        _bulletLifeTimer += Time.deltaTime;
        if (_bulletLifeTimer > _bulletLifeTime)
        {
            Explode();
        }
        Move();
    }

    private void Move()
    {
        _rb.velocity = Vector2.right * _bulletDirection * _bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemies>())
        {
            Explode();
        }
    }

    private void Explode()
    {
        // Instantiate(explodeEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
