using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance { get; set; }

        [SerializeField]
        private AudioClip walk;

        [SerializeField]
        private Animator playerAnimator;
        private int _yDirection;
        private int _xDirection;
        private float _xVelocity;
        private float _yVelocity;
        private bool _isMoving;

        private float _playSFXTimer;
        private float _playSFXTime;

        void Awake()
        {
            instance = (instance == null) ? this : instance;
            _playSFXTimer = 0.25f;
            _playSFXTime = _playSFXTimer;
        }

        void Update()
        {
            _isMoving = (_xVelocity != 0 || _yVelocity != 0) ? true : false;
            _playSFXTime += Time.deltaTime;
            if (_isMoving && _playSFXTime > _playSFXTimer)
            {
                _playSFXTime = 0;
                SFXManager.instance.PlaySoundFXClip(walk, transform, .5f);
            }
            playerAnimator.SetFloat("_yDirection", _yDirection);
            playerAnimator.SetFloat("_xDirection", _xDirection);
            playerAnimator.SetBool("move", _isMoving);
        }

        public float GetHorizontalRaw()
        {
            float input = Input.GetAxisRaw("Horizontal");
            if (_xVelocity != 0)
            {
                _yDirection = 0;
                _yVelocity = 0;
            }
            if (input != 0)
            {
                _xDirection = (int)input;
            }
            _xVelocity = input;
            return input;
        }

        public float GetVerticalRaw()
        {
            float input = Input.GetAxisRaw("Vertical");
            if (_yVelocity != 0)
            {
                _xDirection = 0;
                _xVelocity = 0;
            }
            if (input != 0)
            {
                _yDirection = (int)input;
            }
            _yVelocity = input;
            return input;
        }
    }
}
