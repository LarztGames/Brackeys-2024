using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    [CreateAssetMenu(fileName = "Trap", menuName = "Room/Trap", order = 0)]
    public class Traps : ScriptableObject
    {
        public GameObject gameObject;
        public Sprite sprite;

        private void OnValidate()
        {
            // Validar que el objeto `loot` no esté vacío
            if (gameObject == null)
            {
                Debug.LogWarning($"El campo 'loot' está vacío en el ScriptableObject '{name}'");
            }

            // Validar que el sprite no esté vacío
            if (sprite == null)
            {
                Debug.LogWarning($"El campo 'sprite' está vacío en el ScriptableObject '{name}'");
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }
    }
}
