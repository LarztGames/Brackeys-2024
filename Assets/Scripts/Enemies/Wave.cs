using System.Collections;
using System.Collections.Generic;
using Enemy;
using Managers;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField]
    private GameObject walkEnemy;

    [SerializeField]
    private GameObject flyEnemy;

    [SerializeField]
    private float timeToSpawnEnemies;

    [SerializeField]
    private int amountWalkEnemiesToSpawn;

    [SerializeField]
    private int amountFlyEnemiesToSpawn;

    [SerializeField]
    private Transform[] enemyFlySpawnPoints = new Transform[2];

    [SerializeField]
    private Transform[] enemyWalkSpawnPoints = new Transform[2];

    private int _walkEnemiesToSpawn;
    private int _flyEnemiesToSpawn;
    private float _spawnTimer;

    [SerializeField]
    private float spawnDelay = 0.35f; // Delay between enemy spawns

    void Start()
    {
        _walkEnemiesToSpawn = amountWalkEnemiesToSpawn;
        _flyEnemiesToSpawn = amountFlyEnemiesToSpawn;
        _spawnTimer = 0;
    }

    private void OnEnable()
    {
        _walkEnemiesToSpawn = amountWalkEnemiesToSpawn;
        _flyEnemiesToSpawn = amountFlyEnemiesToSpawn;
        _spawnTimer = 0;
    }

    void Update()
    {
        if (_walkEnemiesToSpawn > 0 || _flyEnemiesToSpawn > 0)
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer >= timeToSpawnEnemies)
            {
                StartCoroutine(StartWave());
                _spawnTimer = 0;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator StartWave()
    {
        // Spawn fly enemies
        int flyEnemiesToSpawn = Mathf.Min(
            Random.Range(1, _flyEnemiesToSpawn + 1),
            _flyEnemiesToSpawn
        );
        _flyEnemiesToSpawn -= flyEnemiesToSpawn;
        for (int i = 0; i < flyEnemiesToSpawn; i++)
        {
            int randomPos = Random.Range(0, enemyFlySpawnPoints.Length);
            Instantiate(flyEnemy, enemyFlySpawnPoints[randomPos].position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay); // Wait for a short time before spawning the next enemy
        }

        // Spawn walk enemies
        int walkEnemiesToSpawn = Mathf.Min(
            Random.Range(1, _walkEnemiesToSpawn + 1),
            _walkEnemiesToSpawn
        );
        _walkEnemiesToSpawn -= walkEnemiesToSpawn;
        for (int i = 0; i < walkEnemiesToSpawn; i++)
        {
            int randomPos = Random.Range(0, enemyWalkSpawnPoints.Length);
            Instantiate(walkEnemy, enemyWalkSpawnPoints[randomPos].position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay); // Wait for a short time before spawning the next enemy
        }
    }
}
