using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect
{
    public enum LootType
    {
        Silver,
        Zafiro,
        Opalo
    }

    [CreateAssetMenu(menuName = "Collectable/Resources", fileName = "Collectable")]
    public class SOCollectableResource : SOCollectable
    {
        public float weight;
        public float amount;
        public LootType lootType;

        // Interactions with the player
        public override void Interact(GameObject resource, GameObject objectThatInteract)
        {
            if (_playerEffects == null || _playerPocket == null)
            {
                GetReferences(objectThatInteract);
            }
            if (_playerPocket.TryAddResource(resource))
            {
                _playerEffects.PlayCollectEffect(collectColor, collectFlashTime, audioClip);
                Destroy(resource);
            }
        }
    }
}
