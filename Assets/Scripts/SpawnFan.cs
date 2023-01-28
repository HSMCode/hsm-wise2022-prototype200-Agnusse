using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnFan : MonoBehaviour
{
    // Array for enemy prefabs to randomly choose from
    [SerializeField] GameObject[] Enemies;

    //where are the fans being spawned
    [SerializeField] float _enemySpawnBoundsMax = 50f;

    // Counter for spawned enemies
    [SerializeField] int _spawnedEnemies;

    // set repeat rate 
    [SerializeField] float _repeatRateOnStart = 1f;


    // Start is called before the first frame update
    void Start()
    {
        // With Invoke Repeating only the parameters generated inside the method can be controlled
        InvokeRepeating("SpawningEnemies", 1f, _repeatRateOnStart);
    }

    private Vector3 GenerateSpawnPosition()
    {
        // Creates a random enemy spawn position 
        Vector3 enemySpawnPos = new Vector3(Random.Range(-_enemySpawnBoundsMax, _enemySpawnBoundsMax), 0, Random.Range(-_enemySpawnBoundsMax, _enemySpawnBoundsMax));

        // returns the random position for the enemy
        return enemySpawnPos;
    }

    // runs script when method SpawningEnemies is called
    void SpawningEnemies()
    {
        // modefies the spawn rate by randomly spawning no enemy
        float repeatRateModifier = Random.Range(0, 10);

        //runs script if the repeatRateModifier is 10 or less
        if (repeatRateModifier <= 10)
        {
            // Get a random slot from the enemy prefab array
            int number = Random.Range(0, Enemies.Length);

            // Instantiate a clone from the prefab enemies at the previously generated position
            Instantiate(Enemies[number], GenerateSpawnPosition(), Enemies[number].transform.rotation);

            // adds 1 ro _spawnedEnemies
            _spawnedEnemies++;
        }
    }
}