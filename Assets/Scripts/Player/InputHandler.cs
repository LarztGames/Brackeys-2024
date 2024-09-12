using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance { get; set; }

        [SerializeField]
        private Animator playerAnimator;
        private int _yDirection;
        private int _xDirection;
        private float _xVelocity;
        private float _yVelocity;
        private bool _isMoving;

        void Awake()
        {
            instance = (instance == null) ? this : instance;
        }

        void Update()
        {
            _isMoving = (_xVelocity != 0 || _yVelocity != 0) ? true : false;
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
