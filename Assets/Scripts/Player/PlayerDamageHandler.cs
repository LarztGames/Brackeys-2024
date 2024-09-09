using System;
using System.Collections;
using System.Collections.Generic;
using Traps;
using UnityEngine;

namespace Player
{
    public class PlayerDamageHandler : MonoBehaviour
    {
        private PlayerEffects _playerEffects;

        [SerializeField]
        private float damageGraceTime;
        private float _damageGraceTimer;

        void Start()
        {
            _playerEffects = GetComponent<PlayerEffects>();
            _damageGraceTimer = damageGraceTime;
        }

        private void Update()
        {
            _damageGraceTimer += Time.deltaTime;
        }

        public void TryReceiveDamage(SOTraps traps)
        {
            if (_damageGraceTimer < damageGraceTime)
            {
                Debug.Log("Already Taken Damage");
                return;
            }
            _damageGraceTimer = 0;
            _playerEffects.PlayDamageEffect(traps.damageColor, traps.damageFlashTime);
        }
    }
}
