using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Dungeon
{
    public class TrapBehaviour : Interactable
    {
        // TODO: Add a grace time to prevent double damage
        protected override void Behaviour(GameObject player)
        {
            player.GetComponent<Pocket>().TryRemoveLoot();
        }
    }
}
