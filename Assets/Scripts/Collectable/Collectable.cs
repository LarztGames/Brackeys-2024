using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(CollectableTriggerHandler))]
    public class Collectable : MonoBehaviour
    {
        [SerializeField]
        private SOCollectable _collectable;

        void Reset()
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
        }

        public void Interact(GameObject objectThatInteract)
        {
            _collectable.Interact(this.gameObject, objectThatInteract);
        }

        public SOCollectable GetCollectableData() => _collectable;

        private void OnValidate()
        {
            if (_collectable == null)
            {
                return;
            }
            if (_collectable.sprite != null)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                GetComponent<SpriteRenderer>().sprite = _collectable.sprite;
            }
        }
    }
}
