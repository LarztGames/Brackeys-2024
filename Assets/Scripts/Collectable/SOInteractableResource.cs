using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collect
{
    [CreateAssetMenu(menuName = "Collectable/Resources", fileName = "Collectable")]
    public class SOCollectableResource : SOCollectable
    {
        // Interactions with the player
        public override void Interact(GameObject resource, GameObject objectThatInteract)
        {
            if (_playerEffects == null || _playerPocket == null)
            {
                GetReferences(objectThatInteract);
            }
            if (_playerPocket.TryAddResource(resource))
            {
                _playerEffects.PlayCollectEffect(collectColor, collectFlashTime);
                Destroy(resource);
            }
        }
    }
}
