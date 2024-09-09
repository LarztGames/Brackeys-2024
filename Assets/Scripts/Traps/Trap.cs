using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Traps
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(TrapTriggerHandler))]
    public class Trap : MonoBehaviour
    {
        [SerializeField]
        private SOTraps _traps;

        void Reset()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        public void Colision(GameObject objectThatColision)
        {
            _traps.GetReferences(objectThatColision);
            _traps.PlayerDamageHandler().TryReceiveDamage(_traps);
        }
    }
}
