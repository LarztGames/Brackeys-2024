using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

        [SerializeField]
        private GameObject visionArea;
        private Vector3 _visionAreaScale;

        void Start()
        {
            _playerEffects = GetComponent<PlayerEffects>();
            _damageGraceTimer = damageGraceTime;
            _visionAreaScale = visionArea.transform.localScale;
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
            StartCoroutine(LoseVision());
        }

        private IEnumerator LoseVision()
        {
            visionArea.transform.DOScale(_visionAreaScale / 2, 0.5f);
            yield return new WaitForSeconds(2f);
            visionArea.transform.DOScale(_visionAreaScale, 0.5f);
        }
    }
}
