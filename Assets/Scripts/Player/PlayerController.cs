using System;
using System.Collections;
using System.Collections.Generic;
using Dungeon;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Movement
        [Header("Movement")]
        [SerializeField]
        private float speed;
        private Rigidbody2D _rb;
        #endregion

        #region Room
        [Header("Room")]
        [SerializeField]
        private CameraController cameraController;

        [SerializeField]
        private float roomTransitionTime;
        private float _roomTransitionTimer;
        private bool _onRoomTransition;
        private Room _room;
        #endregion

        void Start()
        {
            _onRoomTransition = false;
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (!_onRoomTransition)
            {
                _roomTransitionTimer = 0;
                Movement();
            }
            else
            {
                _rb.velocity = Vector3.zero;
                _roomTransitionTimer += Time.deltaTime;
                if (_roomTransitionTimer >= roomTransitionTime)
                {
                    _onRoomTransition = false;
                }
            }
        }

        private void Movement()
        {
            float horizontal = InputHandler.instance.GetHorizontalRaw();
            float vertical = InputHandler.instance.GetVerticalRaw();
            _rb.velocity = new Vector2(horizontal, vertical).normalized * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Room>() is Room room)
            {
                if (_room != room)
                {
                    Debug.Log($"El jugador esta en la room: {room.name}");
                    _room = room;
                    cameraController.SetRoomPosition(_room.transform);
                    _onRoomTransition = true;
                }
            }
        }
    }
}
