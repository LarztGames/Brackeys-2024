using System.Collections;
using System.Collections.Generic;
using Collect;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Pocket : MonoBehaviour
    {
        [SerializeField]
        private Image pocketImage; // Cambiado a Image para usar fillAmount

        [SerializeField]
        private float capacity; // La capacidad total de la mochila
        private float _currentCapacity; // La capacidad restante en la mochila

        // Rango para drop de loot
        [SerializeField]
        private int pocketDropRadius;

        [SerializeField]
        private int pocketMinRadius;

        private List<SOCollectableResource> _resources = new List<SOCollectableResource>();

        void Start()
        {
            _currentCapacity = 0; // Empezamos con la mochila vacía (capacidad usada es 0)
            UpdatePocketImage(); // Actualizamos el fillAmount
        }

        public bool TryAddResource(GameObject resource)
        {
            Collectable collectable = resource.GetComponent<Collectable>();
            SOCollectableResource collectableData = collectable.GetCollectableData();

            // Si añadir el recurso excede la capacidad, rechazamos el recurso
            if ((_currentCapacity + collectableData.weight) > capacity)
            {
                Debug.Log("Pocket is full");
                return false;
            }

            // Añadir el recurso y actualizar la capacidad usada
            _resources.Add(collectableData);
            _currentCapacity += collectableData.weight; // Sumamos al total de recursos recogidos

            UpdatePocketImage(); // Actualizamos el fillAmount de la imagen
            return true;
        }

        public bool TryRemoveLoot()
        {
            if (_resources.Count == 0)
            {
                Debug.Log("Pocket is empty");
                return false;
            }

            // Remover un recurso aleatorio de la mochila
            int index = Random.Range(0, _resources.Count);
            SOCollectableResource collectableData = _resources[index];
            _resources.Remove(collectableData);
            _currentCapacity -= collectableData.weight; // Restamos el peso del recurso removido

            UpdatePocketImage(); // Actualizamos el fillAmount
            return true;
        }

        public bool ClearPocket()
        {
            if (_resources.Count == 0)
            {
                Debug.Log("Pocket is empty");
                return false;
            }

            // Limpiar la mochila y resetear la capacidad
            _resources.Clear();
            _currentCapacity = 0; // La mochila está vacía

            UpdatePocketImage(); // Actualizamos el fillAmount
            return true;
        }

        private void DropLoot()
        {
            // Código para dropear recursos
            float angle = Random.Range(pocketMinRadius, Mathf.PI * 2);
            float distance = Random.Range(pocketMinRadius, pocketDropRadius);
            float x = Mathf.Cos(angle) * distance;
            float y = Mathf.Sin(angle) * distance;
            Vector2 position = new Vector2(x, y) + (Vector2)transform.position;
            int index = Random.Range(0, _resources.Count);
            Instantiate(_resources[index].gameObject, position, Quaternion.identity);
        }

        // Este método se asegura de que la barra visual refleje el llenado de la mochila
        private void UpdatePocketImage()
        {
            // El fillAmount debe ser el porcentaje de la capacidad total que ha sido usada
            pocketImage.fillAmount = Mathf.Clamp01(_currentCapacity / capacity);
        }

        // Devuelve la diferencia de capacidad (usado vs total)
        public float GetCapacityDiference() => 1 - (_currentCapacity / capacity);

        // Devuelve los recursos actuales en la mochila
        public List<SOCollectableResource> GetResources() => _resources;
    }
}
