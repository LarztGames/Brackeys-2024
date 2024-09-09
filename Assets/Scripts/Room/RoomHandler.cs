using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class RoomHandler : MonoBehaviour
    {
        public static RoomHandler instance { get; set; }

        [SerializeField]
        private Room[] rooms;

        void Awake()
        {
            instance = (instance == null) ? this : instance;
            rooms = GetComponentsInChildren<Room>();
        }

        public void ReloadDungeon()
        {
            foreach (Room room in rooms)
            {
                room.ReloadRoom();
            }
        }
    }
}
