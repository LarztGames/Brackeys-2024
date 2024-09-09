using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class Room : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> resources = new List<GameObject>();

        [SerializeField]
        private List<Transform> resourceSpawnPoints = new List<Transform>();

        [SerializeField]
        private List<GameObject> traps = new List<GameObject>();

        [SerializeField]
        private List<Transform> trapSpawnPoints = new List<Transform>();

        private List<GameObject> roomObjects = new List<GameObject>();

        void Start()
        {
            GenerateLoot();
            GenerateTraps();
        }

        public void ReloadRoom()
        {
            foreach (GameObject item in roomObjects)
            {
                Destroy(item);
            }
            GenerateLoot();
            GenerateTraps();
        }

        private void GenerateLoot()
        {
            int lootToSpawn = UnityEngine.Random.Range(1, resourceSpawnPoints.Count);
            for (int i = 0; i < lootToSpawn; i++)
            {
                int randomLoot = UnityEngine.Random.Range(0, resources.Count);
                int randomPosition = UnityEngine.Random.Range(0, resourceSpawnPoints.Count);
                GameObject loot = Instantiate(
                    resources[randomLoot],
                    resourceSpawnPoints[randomPosition].position,
                    Quaternion.identity
                );
                loot.transform.parent = transform;
                resourceSpawnPoints.Remove(resourceSpawnPoints[randomPosition]);
                roomObjects.Add(loot);
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
                    traps[randomTrap],
                    trapSpawnPoints[randomPosition].position,
                    Quaternion.identity
                );
                trap.transform.parent = transform;
                trapSpawnPoints.Remove(trapSpawnPoints[randomPosition]);
                roomObjects.Add(trap);
            }
        }
    }
}
