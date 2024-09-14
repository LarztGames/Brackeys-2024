using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect
{
    public abstract class SOCollectable : ScriptableObject
    {
        [Header("Collectable Values")]
        public GameObject gameObject;
        public Sprite sprite;

        [Header("Collection Effects")]
        public Color collectColor;
        public float collectFlashTime;
        public AudioClip audioClip;

        // public AudioClip collectClip;

        protected Player.PlayerEffects _playerEffects;
        protected Player.Pocket _playerPocket;

        public abstract void Interact(GameObject gameObject, GameObject objectThatInteract);

        public void GetReferences(GameObject objectThatInteract)
        {
            _playerEffects = objectThatInteract.GetComponent<Player.PlayerEffects>();
            _playerPocket = objectThatInteract.GetComponent<Player.Pocket>();
        }
    }
}
