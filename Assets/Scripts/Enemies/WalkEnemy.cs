using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class WalkEnemy : BaseEnemy
    {
        private float _direction;

        void Start()
        {
            _currentState = EnemyState.Moving;
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _currentHealth = data.health;
            _receivingDamageTimer = GetAnimationDuration("WalkReceiveDamage");
            _maxAliveTimer = 0;
        }

        void Update()
        {
            this.PlayAnimation();
            this.Direction();
            Die();
            if (IsMoving)
            {
                this.Movement();
                CheckRangeAttack();
            }
            if (IsAttacking)
            {
                _rb.velocity = Vector2.zero;
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
            _rb.velocity = Vector2.right * _direction * data.moveSpeed;
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
            _direction = (transform.position.x > 0) ? -1 : 1;
            if (_direction < 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
    }
}
