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

        [SerializeField]
        private float minSpeed;

        private Rigidbody2D _rb;
        private Pocket _pocket;
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
            // Start position on load
            _onRoomTransition = false;
            _rb = GetComponent<Rigidbody2D>();
            _pocket = GetComponent<Pocket>();
        }

        void Update()
        {
            Movement();
            TryRoomTransition();
        }

        private void Movement()
        {
            float horizontal = InputHandler.instance.GetHorizontalRaw();
            float vertical = InputHandler.instance.GetVerticalRaw();
            float modifiedSpeed = Mathf.Max(speed * _pocket.GetCapacityDiference(), minSpeed);
            _rb.velocity = new Vector2(horizontal, vertical).normalized * modifiedSpeed;
        }

        private void TryRoomTransition()
        {
            if (!_onRoomTransition)
            {
                _roomTransitionTimer = 0;
                return;
            }
            _rb.velocity = Vector3.zero;
            _roomTransitionTimer += Time.deltaTime;
            if (_roomTransitionTimer >= roomTransitionTime)
            {
                _onRoomTransition = false;
            }
        }

        // Comprueba cuando se entra en una habitacion
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Room>() is Room room)
            {
                if (_room != room)
                {
                    // Debug.Log($"El jugador esta en la room: {room.name}");
                    _room = room;
                    cameraController.SetRoomPosition(_room.transform);
                    _onRoomTransition = true;
                }
            }

            if (other.gameObject.GetComponent<BackToLaboratory>())
            {
                StorageManager.instance.AddLoot(_pocket.GetResources());
                _pocket.ClearPocket();
                transform.position = new Vector2(0, 3.4f);
            }
        }
    }
}
