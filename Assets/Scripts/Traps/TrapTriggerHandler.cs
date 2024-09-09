using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traps
{
    public class TrapTriggerHandler : MonoBehaviour
    {
        private Trap _trap;

        void Awake()
        {
            _trap = GetComponent<Trap>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _trap.Colision(other.gameObject);
                Debug.Log("Colision with player");
            }
        }
    }
}
