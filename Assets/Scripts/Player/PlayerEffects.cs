using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class PlayerEffects : MonoBehaviour
    {
        private bool _isEffecting;
        private SpriteRenderer _spriteRenderer;
        private Coroutine _effectCoroutine;
        private Color _baseColor;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _baseColor = _spriteRenderer.color;
        }

        // TODO: Add sound
        public void PlayCollectEffect(Color color, float duration)
        {
            if (_isEffecting)
            {
                StopCoroutine(_effectCoroutine);
                _isEffecting = false;
            }
            _effectCoroutine = StartCoroutine(CollectEffect(color, duration));
        }

        public void PlayDamageEffect(Color color, float duration)
        {
            if (_isEffecting)
            {
                StopCoroutine(_effectCoroutine);
                _isEffecting = false;
            }
            _effectCoroutine = StartCoroutine(DamageEffect(color, duration));
        }

        private IEnumerator CollectEffect(Color color, float duration)
        {
            _isEffecting = true;
            _spriteRenderer.DOColor(color, duration);
            gameObject.transform.DOScale(1f, duration);
            yield return new WaitForSeconds(duration);
            gameObject.transform.DOScale(0.8f, duration);
            _spriteRenderer.DOColor(_baseColor, duration);
            _isEffecting = false;
        }

        private IEnumerator DamageEffect(Color color, float duration)
        {
            _isEffecting = true;
            _spriteRenderer.DOColor(color, 0);
            yield return new WaitForSeconds(duration);
            _spriteRenderer.DOColor(_baseColor, 0);
            yield return new WaitForSeconds(duration);
            _spriteRenderer.DOColor(color, 0);
            yield return new WaitForSeconds(duration);
            _spriteRenderer.DOColor(_baseColor, 0);
            _isEffecting = false;
        }
    }
}
