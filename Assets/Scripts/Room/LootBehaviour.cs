using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Dungeon
{
    public class LootBehaviour : Interactable
    {
        [SerializeField]
        private Loot loot;

        protected override void Behaviour(GameObject player)
        {
            player.GetComponent<Pocket>().TryAddLoot(gameObject, loot);
        }
    }
}
