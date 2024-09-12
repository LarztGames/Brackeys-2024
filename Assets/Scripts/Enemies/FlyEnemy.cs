using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class FlyEnemy : BaseEnemy
    {
        [SerializeField]
        private float oscilationRange;

        private float _direction;

        void Start()
        {
            _currentState = EnemyState.Moving;
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _rb.gravityScale = 0;
            _currentHealth = data.health;
            _receivingDamageTimer = GetAnimationDuration("FlyReceiveDamage");
            _maxAliveTimer = 0;
        }

        void Update()
        {
            this.PlayAnimation();
            this.Direction();
            this.Movement();
            Die();
            CheckRangeAttack();
            if (IsAttacking)
            {
                Attack();
            }
            _maxAliveTimer += Time.deltaTime;
            if (_maxAliveTimer > data.maxTimeAlive)
            {
                Die();
            }
        }

        protected override void Movement()
        {
            // Movimiento horizontal basado en la velocidad
            Vector2 horizontalMovement = Vector2.right * _direction * data.moveSpeed;

            // Movimiento vertical oscilante usando Mathf.Sin para generar una onda
            float verticalOscillation = Mathf.Sin(Time.time * 2) * oscilationRange;

            // La nueva posición es la combinación del movimiento horizontal y la oscilación vertical
            _rb.velocity = new Vector2(horizontalMovement.x, verticalOscillation);
        }

        protected override void Attack()
        {
            // Do dame to laboratory
            Debug.Log($"{gameObject.name} attack {_targetCollider.name}");
            // Wait for seconds for next attack
            _targetCollider.GetComponent<Laboratory>().ReceiveDamage(data.damage);
        }

        protected override void Direction()
        {
            if (transform.position.x <= -30)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                _direction = 1;
            }
            if (transform.position.x >= 30)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                _direction = -1;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, oscilationRange);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, data.rangeAttack);
        }
    }
}
