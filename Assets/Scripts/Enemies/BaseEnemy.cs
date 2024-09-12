using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public enum EnemyState
    {
        Attacking,
        Moving,
        ReceivingDamage,
        Dying
    }

    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField]
        protected SOEnemy data;

        protected Rigidbody2D _rb;

        protected EnemyState _currentState;
        protected EnemyState _lastState;

        protected Animator _animator;

        protected Collider2D _collider;
        protected Collider2D _targetCollider;

        protected float _currentHealth;
        protected float _receivingDamageTimer;
        protected float _maxAliveTimer;

        public bool IsAttacking => _currentState == EnemyState.Attacking;
        public bool IsMoving => _currentState == EnemyState.Moving;
        public bool IsReceivingDamage => _currentState == EnemyState.ReceivingDamage;
        public bool IsDying => _currentState == EnemyState.Dying;

        protected abstract void Direction();
        protected abstract void Movement();
        protected abstract void Attack();

        protected IEnumerator ReceivingDamage(float damageReceive)
        {
            _animator.SetBool("damage", true);
            _currentHealth -= damageReceive;
            yield return new WaitForSeconds(_receivingDamageTimer);
            _animator.SetBool("damage", false);
        }

        protected void Die()
        {
            if (_currentHealth <= 0)
            {
                // TODO: Animation
                Destroy(gameObject);
            }
        }

        protected void CheckRangeAttack()
        {
            if (_targetCollider != null)
            {
                ColliderDistance2D colliderDistance = _collider.Distance(_targetCollider);
                if (colliderDistance.distance < data.attackRate)
                {
                    _currentState = EnemyState.Attacking;
                }
                else
                {
                    _currentState = EnemyState.Moving;
                }
            }
            else
            {
                _currentState = EnemyState.Moving;
            }
        }

        protected void PlayAnimation()
        {
            _animator.SetBool("move", IsMoving);
            _animator.SetBool("attack", IsAttacking);
            _animator.SetBool("die", IsDying);
        }

        protected float GetAnimationDuration(string animName)
        {
            RuntimeAnimatorController rac = _animator.runtimeAnimatorController;

            // Buscar el clip de animación por nombre
            foreach (var clip in rac.animationClips)
            {
                if (clip.name == animName)
                {
                    return clip.length; // Retorna la duración en segundos
                }
            }

            // Si no encuentra la animación, retorna 0
            Debug.LogError($"Animación '{animName}' no encontrada.");
            return 0f;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, data.rangeAttack);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Lab"))
            {
                _targetCollider = other;
                _targetCollider.GetComponent<Laboratory>().ReceiveDamage(data.damage);
            }

            if (other.gameObject.CompareTag("Bullet"))
            {
                _lastState = _currentState;
                float damageReceive = other.gameObject.GetComponent<Bullet>().GetDoDamage();
                StartCoroutine(ReceivingDamage(damageReceive));
            }

            if (other.gameObject.CompareTag("RadiationBullet"))
            {
                _lastState = _currentState;
                float damageReceive = other
                    .gameObject.GetComponent<RadiationBullet>()
                    .GetDoDamage();
                StartCoroutine(ReceivingDamage(damageReceive));
            }

            if (other.gameObject.CompareTag("AutoBullet"))
            {
                _lastState = _currentState;
                float damageReceive = other.gameObject.GetComponent<AutoBullet>().GetDoDamage();
                StartCoroutine(ReceivingDamage(damageReceive));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Lab"))
            {
                _targetCollider = null;
            }
        }
    }
}
