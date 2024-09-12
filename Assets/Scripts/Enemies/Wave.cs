using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Managers;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();

    // [SerializeField]
    // private float enemiesPerRound;

    [SerializeField]
    private float timeToSpawnEnemies;

    [SerializeField]
    private float amountEnmiesToSpawn;

    [SerializeField]
    private Transform[] enemyFlySpawnPoint = new Transform[2];

    [SerializeField]
    private Transform[] enemyWalkSpawnPoint = new Transform[2];

    void Start()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        Debug.Log("Spawning");
        float currentAmountEnemiesToSpawn = 0;

        if (enemies.Count == 0)
        {
            Debug.LogError("La lista de enemigos está vacía. No se pueden spawnear enemigos.");
            yield break; // Salimos de la corrutina si no hay enemigos
        }

        while (RoundManager.instance.GetRoundState() == RoundState.Storm)
        {
            for (int i = 0; i < amountEnmiesToSpawn; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, enemies.Count);

                if (enemies[randomIndex].GetComponent<FlyEnemy>())
                {
                    int randomFlyPosition = UnityEngine.Random.Range(0, enemyFlySpawnPoint.Length);

                    Instantiate(
                        enemies[randomIndex],
                        enemyFlySpawnPoint[randomFlyPosition].position,
                        Quaternion.identity
                    );
                }
                else
                {
                    int randomWalkPosition = UnityEngine.Random.Range(
                        0,
                        enemyWalkSpawnPoint.Length
                    );

                    Instantiate(
                        enemies[randomIndex],
                        enemyWalkSpawnPoint[randomWalkPosition].position,
                        Quaternion.identity
                    );
                }
            }

            currentAmountEnemiesToSpawn += amountEnmiesToSpawn;

            // Esperamos antes de la próxima tanda de enemigos
            // Debug.Log($"Esperando {timeToSpawnEnemies} segundos antes de la próxima tanda...");
            yield return new WaitForSeconds(timeToSpawnEnemies);
        }

        Debug.Log("Finalizó la oleada de enemigos. Desactivando el objeto.");
        gameObject.SetActive(false); // Opcional: desactiva el spawner
    }
}
