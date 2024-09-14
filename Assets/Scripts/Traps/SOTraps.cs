using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traps
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Traps/Trap", fileName = "SOTraps", order = 0)]
    public class SOTraps : ScriptableObject
    {
        [Header("Damage Effects")]
        public Color damageColor;
        public float damageFlashTime;
        public AudioClip audioClip;

        // public AudioClip collectClip;

        private Player.PlayerDamageHandler playerDamageHandler;

        public void GetReferences(GameObject objectThatColision)
        {
            playerDamageHandler = objectThatColision.GetComponent<Player.PlayerDamageHandler>();
        }

        public Player.PlayerDamageHandler PlayerDamageHandler() => playerDamageHandler;
    }
}
