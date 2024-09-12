using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class RoomManager : MonoBehaviour
    {
        public static RoomManager instance { get; set; }

        [SerializeField]
        private Room[] rooms;

        void Awake()
        {
            instance = (instance == null) ? this : instance;
        }

        public void ReloadDungeon()
        {
            foreach (Room room in rooms)
            {
                Debug.Log($"Reloading room: {room.gameObject.name}");
                room.ReloadRoom();
            }
        }
    }
}
