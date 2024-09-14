using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Enemy", menuName = "Enemy", order = 0)]
    public class SOEnemy : ScriptableObject
    {
        [Header("Enemy values")]
        public float health;
        public float moveSpeed;
        public float rangeAttack;
        public float attackRate;
        public float damage;
        public float maxTimeAlive;
        public AudioClip dieAudio;
        public AudioClip damageAudio;
    }
}
