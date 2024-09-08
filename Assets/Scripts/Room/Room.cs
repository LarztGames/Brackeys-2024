using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class Room : MonoBehaviour
    {
        [SerializeField]
        private List<Loot> loots = new List<Loot>();

        [SerializeField]
        private List<Transform> lootSpawnPoints = new List<Transform>();

        [SerializeField]
        private List<Traps> traps = new List<Traps>();

        [SerializeField]
        private List<Transform> trapSpawnPoints = new List<Transform>();

        void Start()
        {
            GenerateLoot();
            GenerateTraps();
        }

        private void GenerateLoot()
        {
            int lootToSpawn = UnityEngine.Random.Range(1, lootSpawnPoints.Count);
            for (int i = 0; i < lootToSpawn; i++)
            {
                int randomLoot = UnityEngine.Random.Range(0, loots.Count);
                int randomPosition = UnityEngine.Random.Range(0, lootSpawnPoints.Count);
                GameObject loot = Instantiate(
                    loots[randomLoot].gameObject,
                    lootSpawnPoints[randomPosition].position,
                    Quaternion.identity
                );
                loot.transform.parent = transform;
                lootSpawnPoints.Remove(lootSpawnPoints[randomPosition]);
            }
        }

        private void GenerateTraps()
        {
            int trapToSpawn = UnityEngine.Random.Range(1, trapSpawnPoints.Count);
            for (int i = 0; i < trapToSpawn; i++)
            {
                int randomTrap = UnityEngine.Random.Range(0, traps.Count);
                int randomPosition = UnityEngine.Random.Range(0, trapSpawnPoints.Count);
                GameObject trap = Instantiate(
                    traps[randomTrap].gameObject,
                    trapSpawnPoints[randomPosition].position,
                    Quaternion.identity
                );
                trap.transform.parent = transform;
                trapSpawnPoints.Remove(trapSpawnPoints[randomPosition]);
            }
        }
    }
}
