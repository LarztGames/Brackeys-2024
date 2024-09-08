using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Movement();
        }

        private void Movement()
        {
            float horizontal = InputHandler.instance.GetHorizontalRaw();
            float vertical = InputHandler.instance.GetVerticalRaw();
            _rb.velocity = new Vector2(horizontal, vertical).normalized;
        }
    }
}
