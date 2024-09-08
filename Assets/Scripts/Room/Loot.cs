using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    [CreateAssetMenu(fileName = "Loot", menuName = "Room/Loot", order = 0)]
    public class Loot : ScriptableObject
    {
        public GameObject gameObject;
        public Sprite sprite;
        public int weight;

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

            // Validar que el peso sea positivo
            if (weight <= 0)
            {
                Debug.LogWarning(
                    $"El campo 'weight' debe ser un valor positivo en el ScriptableObject '{name}'"
                );
            }
        }
    }
}
