using Enemy;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class RadiationBullet : MonoBehaviour
{
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
    }

    void Update()
    {
        _bulletLifeTimer += Time.deltaTime;
        if (_bulletLifeTimer > _bulletLifeTime)
        {
            Destroy(gameObject);
        }
        Move();
    }

    private void Move()
    {
        transform.localScale = Vector2.Lerp(
            transform.localScale,
            Vector2.one * 12,
            _bulletSpeed * Time.deltaTime
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BaseEnemy>())
        {
            Debug.Log("Trigger with enemy");
        }
    }

    public float GetDoDamage() => _bulletDamage;
}
