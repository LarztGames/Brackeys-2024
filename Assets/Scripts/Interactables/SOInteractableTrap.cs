using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Interact
{
    [CreateAssetMenu(menuName = "Interactable/Trap", fileName = "Interactable")]
    public class SOInteractableTrap : SOInteractable
    {
        public float damageGraceTime;
        private float graceTimer;

        void Start()
        {
            graceTimer = damageGraceTime;
        }

        // Interactions with the player
        public override void Interact(GameObject resource, GameObject objectThatInteract)
        {
            if (_playerEffects == null || _playerPocket == null)
            {
                GetReferences(objectThatInteract);
            }
            while (graceTimer <= damageGraceTime)
            {
                graceTimer += Time.deltaTime;
            }
            if (graceTimer >= damageGraceTime)
            {
                graceTimer = 0;
                _playerPocket.TryRemoveLoot();
                _playerEffects.PlayEffect(collectColor, collectFlashTime);
            }
        }
    }
}
