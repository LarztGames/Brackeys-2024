using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler instance { get; set; }

        void Awake()
        {
            instance = (instance == null) ? this : instance;
        }

        public float GetHorizontalRaw()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        public float GetVerticalRaw()
        {
            return Input.GetAxisRaw("Vertical");
        }
    }
}
