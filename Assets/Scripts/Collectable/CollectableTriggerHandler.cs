using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect
{
    public class CollectableTriggerHandler : MonoBehaviour
    {
        private Collectable _collectable;

        void Awake()
        {
            _collectable = GetComponent<Collectable>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _collectable.Interact(other.gameObject);
            }
        }
    }
}
