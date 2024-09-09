using System.Collections;
using System.Collections.Generic;
using Collect;
using UnityEngine;

namespace Player
{
    public class Pocket : MonoBehaviour
    {
        [SerializeField]
        private float capacity;
        private float _currentCapacity;

        // [SerializeField]
        private int pocketDropRadius;

        // [SerializeField]
        private int pocketMinRadius;

        [SerializeField]
        private List<SOCollectable> resources = new List<SOCollectable>();

        void Start()
        {
            _currentCapacity = capacity;
        }

        public bool TryAddResource(GameObject resource)
        {
            Collectable collectable = resource.GetComponent<Collectable>();
            SOCollectable collectableData = collectable.GetCollectableData();
            Debug.Log(collectableData.weight);
            if (_currentCapacity == 0 || (_currentCapacity - collectableData.weight) < 0)
            {
                Debug.Log("Pocket is full");
                return false;
            }
            resources.Add(collectableData);
            _currentCapacity -= collectableData.weight;
            return true;
        }

        public bool TryRemoveLoot()
        {
            if (resources.Count == 0)
            {
                Debug.Log("Pocket is empty");
                return false;
            }
            int index = Random.Range(0, resources.Count);
            SOCollectable collectableData = resources[index];
            resources.Remove(collectableData);
            _currentCapacity += collectableData.weight;
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
            int index = Random.Range(0, resources.Count);
            Instantiate(resources[index].gameObject, position, Quaternion.identity);
        }

        public float GetCapacityDiference() => _currentCapacity / capacity;
    }
}
