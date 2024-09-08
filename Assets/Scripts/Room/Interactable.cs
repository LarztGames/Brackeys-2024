using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public abstract class Interactable : MonoBehaviour
    {
        protected abstract void Behaviour(GameObject player);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Behaviour(other.gameObject);
            }
        }
    }
}
