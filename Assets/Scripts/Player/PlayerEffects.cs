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
        private Vector3 _baseScale;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _baseColor = _spriteRenderer.color;
            _baseScale = transform.localScale;
        }

        // TODO: Add sound
        public void PlayCollectEffect(Color color, float duration, AudioClip audioClip)
        {
            if (_isEffecting)
            {
                StopCoroutine(_effectCoroutine);
                _isEffecting = false;
            }
            _effectCoroutine = StartCoroutine(CollectEffect(color, duration, audioClip));
        }

        public void PlayDamageEffect(Color color, float duration, AudioClip audioClip)
        {
            if (_isEffecting)
            {
                StopCoroutine(_effectCoroutine);
                _isEffecting = false;
            }
            _effectCoroutine = StartCoroutine(DamageEffect(color, duration, audioClip));
        }

        private IEnumerator CollectEffect(Color color, float duration, AudioClip audioClip)
        {
            SFXManager.instance.PlaySoundFXClip(audioClip, transform);
            _isEffecting = true;
            _spriteRenderer.DOColor(color, duration);
            gameObject.transform.DOScale(_baseScale * 1.2f, duration);
            yield return new WaitForSeconds(duration);
            gameObject.transform.DOScale(_baseScale, duration);
            _spriteRenderer.DOColor(_baseColor, duration);
            _isEffecting = false;
        }

        private IEnumerator DamageEffect(Color color, float duration, AudioClip audioClip)
        {
            SFXManager.instance.PlaySoundFXClip(audioClip, transform);
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
