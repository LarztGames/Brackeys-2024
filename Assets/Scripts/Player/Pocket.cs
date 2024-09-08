using System.Collections;
using System.Collections.Generic;
using Dungeon;
using UnityEngine;

namespace Player
{
    public class Pocket : MonoBehaviour
    {
        [SerializeField]
        private int capacity;

        [SerializeField]
        private List<Loot> loots = new List<Loot>();

        public void TryAddLoot(Loot loot)
        {
            if (capacity == 0 && capacity < loot.weight)
            {
                Debug.Log("Pocket is full");
                return;
            }
            loots.Add(loot);
            capacity -= loot.weight;
        }

        public void TryRemoveLoot()
        {
            if (loots.Count == 0)
            {
                Debug.Log("Pocket is empty");
                return;
            }
            int lootInxed = Random.Range(0, loots.Count);
            Loot loot = loots[lootInxed];
            loots.Remove(loot);
            capacity += loot.weight;
        }
    }
}
