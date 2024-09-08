using System.Collections;
using System.Collections.Generic;
using Dungeon;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class Pocket : MonoBehaviour
    {
        [SerializeField]
        private int capacity;
        private int _currentCapacity;

        // [SerializeField]
        private int pocketDropRadius;

        // [SerializeField]
        private int pocketMinRadius;

        [SerializeField]
        private List<Loot> loots = new List<Loot>();

        void Start()
        {
            _currentCapacity = capacity;
        }

        public void TryAddLoot(GameObject lootObject, Loot lootData)
        {
            if (_currentCapacity == 0 || (_currentCapacity - lootData.weight) <= 0)
            {
                Debug.Log("Pocket is full");
                return;
            }
            loots.Add(lootData);
            _currentCapacity -= lootData.weight;
            Destroy(lootObject);
        }

        public void TryRemoveLoot()
        {
            if (loots.Count == 0)
            {
                Debug.Log("Pocket is empty");
                return;
            }
            int lootInxed = Random.Range(0, loots.Count);
            Loot loot = loots[lootInxed];
            loots.Remove(loot);
            _currentCapacity += loot.weight;
        }

        // Tiene bugs, se puede instanciar objetos dentro de la pared, tal vez sea mejor que lo dropea siempre hacia abajo
        // O calcular si en la posicion que se va a generar el objeto hay una colision con una pared o una trampa y que lo vuelva a cambiar
        private void DropLoot(Loot loot)
        {
            float angle = Random.Range(pocketMinRadius, Mathf.PI * 2);
            float distance = Random.Range(pocketMinRadius, pocketDropRadius);
            float x = Mathf.Cos(angle) * distance;
            float y = Mathf.Sin(angle) * distance;
            Vector2 position = new Vector2(x, y) + (Vector2)transform.position;
            Instantiate(loot.gameObject, position, Quaternion.identity);
        }

        public float GetCapacityDiference() => (float)_currentCapacity / (float)capacity;
    }
}
