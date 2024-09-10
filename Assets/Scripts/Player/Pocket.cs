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
        private Slider pocketSlider;

        [SerializeField]
        private float capacity;
        private float _currentCapacity;

        // [SerializeField]
        private int pocketDropRadius;

        // [SerializeField]
        private int pocketMinRadius;

        private List<SOCollectableResource> _resources = new List<SOCollectableResource>();

        void Start()
        {
            pocketSlider.maxValue = capacity;
            pocketSlider.value = 0;
            _currentCapacity = capacity;
        }

        public bool TryAddResource(GameObject resource)
        {
            Collectable collectable = resource.GetComponent<Collectable>();
            SOCollectableResource collectableData = collectable.GetCollectableData();
            Debug.Log(collectableData);
            if (_currentCapacity == 0 || (_currentCapacity - collectableData.weight) < 0)
            {
                Debug.Log("Pocket is full");
                return false;
            }
            _resources.Add(collectableData);
            _currentCapacity -= collectableData.weight;
            pocketSlider.value += collectableData.weight;
            return true;
        }

        public bool TryRemoveLoot()
        {
            if (_resources.Count == 0)
            {
                Debug.Log("Pocket is empty");
                return false;
            }
            int index = Random.Range(0, _resources.Count);
            SOCollectableResource collectableData = _resources[index];
            _resources.Remove(collectableData);
            _currentCapacity += collectableData.weight;
            return true;
        }

        public bool ClearPocket()
        {
            if (_resources.Count == 0)
            {
                Debug.Log("Pocket is empty");
                return false;
            }
            _resources.Clear();
            _currentCapacity = capacity;
            return true;
        }

        // Tiene bugs, se puede instanciar objetos dentro de la pared, tal vez sea mejor que lo dropea siempre hacia abajo
        // O calcular si en la posicion que se va a generar el objeto hay una colision con una pared o una trampa y que lo vuelva a cambiar
        private void DropLoot()
        {
            float angle = Random.Range(pocketMinRadius, Mathf.PI * 2);
            float distance = Random.Range(pocketMinRadius, pocketDropRadius);
            float x = Mathf.Cos(angle) * distance;
            float y = Mathf.Sin(angle) * distance;
            Vector2 position = new Vector2(x, y) + (Vector2)transform.position;
            int index = Random.Range(0, _resources.Count);
            Instantiate(_resources[index].gameObject, position, Quaternion.identity);
        }

        public float GetCapacityDiference() => _currentCapacity / capacity;

        public List<SOCollectableResource> GetResources() => _resources;
    }
}
