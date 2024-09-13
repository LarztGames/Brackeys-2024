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
        private List<Transform> _resourceCopySpawnPoints = new List<Transform>();

        [SerializeField]
        private List<GameObject> traps = new List<GameObject>();

        [SerializeField]
        private List<Transform> trapSpawnPoints = new List<Transform>();
        private List<Transform> _trapCopySpawnPoints = new List<Transform>();

        [SerializeField]
        private List<GameObject> doors = new List<GameObject>();

        [SerializeField]
        private List<Transform> doorSpawnPoints = new List<Transform>();
        private List<Transform> _doorCopySpawnPoints = new List<Transform>();

        private List<GameObject> roomObjects = new List<GameObject>();

        void Start()
        {
            ReloadRoom();
        }

        public void ReloadRoom()
        {
            _resourceCopySpawnPoints = CreateListCopy(resourceSpawnPoints);
            _trapCopySpawnPoints = CreateListCopy(trapSpawnPoints);
            _doorCopySpawnPoints = CreateListCopy(doorSpawnPoints);
            if (roomObjects.Count != 0)
            {
                foreach (GameObject item in roomObjects)
                {
                    if (item != null)
                    {
                        Destroy(item);
                    }
                }
            }
            GenerateLoot();
            GenerateTraps();
            GenerateDoors();
        }

        private void GenerateDoors()
        {
            if (_doorCopySpawnPoints.Count <= 0)
            {
                return;
            }
            int doorsToSpawn = UnityEngine.Random.Range(0, _doorCopySpawnPoints.Count + 1);
            for (int i = 0; i < doorsToSpawn; i++)
            {
                int randomDoor = UnityEngine.Random.Range(0, doors.Count);
                int randomPosition = UnityEngine.Random.Range(0, _doorCopySpawnPoints.Count);
                GameObject door = Instantiate(
                    doors[randomDoor],
                    _doorCopySpawnPoints[randomPosition].position,
                    Quaternion.identity
                );
                door.transform.parent = transform;
                _doorCopySpawnPoints.Remove(_doorCopySpawnPoints[randomPosition]);
                roomObjects.Add(door);
            }
        }

        private List<Transform> CreateListCopy(List<Transform> original)
        {
            List<Transform> copy = new List<Transform>();
            foreach (Transform item in original)
            {
                copy.Add(item);
            }
            return copy;
        }

        private void GenerateLoot()
        {
            if (_resourceCopySpawnPoints.Count <= 0)
            {
                return;
            }
            int lootToSpawn = UnityEngine.Random.Range(1, _resourceCopySpawnPoints.Count + 1);
            for (int i = 0; i < lootToSpawn; i++)
            {
                int randomLoot = UnityEngine.Random.Range(0, resources.Count);
                int randomPosition = UnityEngine.Random.Range(0, _resourceCopySpawnPoints.Count);
                GameObject loot = Instantiate(
                    resources[randomLoot],
                    _resourceCopySpawnPoints[randomPosition].position,
                    Quaternion.identity
                );
                loot.transform.parent = transform;
                _resourceCopySpawnPoints.Remove(_resourceCopySpawnPoints[randomPosition]);
                roomObjects.Add(loot);
            }
        }

        private void GenerateTraps()
        {
            if (_trapCopySpawnPoints.Count <= 0)
            {
                return;
            }
            int trapToSpawn = UnityEngine.Random.Range(1, _trapCopySpawnPoints.Count + 1);
            for (int i = 0; i < trapToSpawn; i++)
            {
                int randomTrap = UnityEngine.Random.Range(0, traps.Count);
                int randomPosition = UnityEngine.Random.Range(0, _trapCopySpawnPoints.Count);
                GameObject trap = Instantiate(
                    traps[randomTrap],
                    _trapCopySpawnPoints[randomPosition].position,
                    Quaternion.identity
                );
                trap.transform.parent = transform;
                _trapCopySpawnPoints.Remove(_trapCopySpawnPoints[randomPosition]);
                roomObjects.Add(trap);
            }
        }
    }
}
