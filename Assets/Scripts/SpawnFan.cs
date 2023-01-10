using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpawnFan : MonoBehaviour
{
    // Array for enemy prefabs to randomly choose from
    [SerializeField] GameObject[] Enemies;
    [SerializeField] float enemySpawnBoundsMax = 50f;

    // Counter for spawned enemies
    [SerializeField] int spawnedEnemies;

    // This repeat rate is only changeable before starting the scene, to change the repeat rate on runtime we need a different solution
    [SerializeField] float repeatRateOnStart = 2f;


    // Start is called before the first frame update
    void Start()
    {
        // With Invoke Repeating only the parameters generated inside the method can be controlled
        InvokeRepeating("SpawningEnemies", 3f, repeatRateOnStart);
    }

    private Vector3 GenerateSpawnPosition()
    {
        // Create a random enemy spawn position 
        Vector3 enemySpawnPos = new Vector3(Random.Range(-enemySpawnBoundsMax, enemySpawnBoundsMax), 1,
            Random.Range(-enemySpawnBoundsMax, enemySpawnBoundsMax));
            
        // Long form writing for the above Vector3 in line 36. This generates both vectors first and then assembles them
        // float enemySpawnPosX = Random.Range(-enemySpawnBoundsMax, enemySpawnBoundsMax);
        // float enemySpawnPosZ = Random.Range(-enemySpawnBoundsMax, enemySpawnBoundsMax);
        // Vector3 enemySpawnPos = new Vector3(enemySpawnPosX, 0, enemySpawnPosZ);

        return enemySpawnPos;
    }

    void SpawningEnemies()
    {
        // NOTE: Little hack to have a bit more control over the repeatRate, we can bind this to the enemies spawned. We'll change this soon.
        float repeatRateModifier = Random.Range(0, 10);

        if (repeatRateModifier <= 5)
        {
            // Get a random slot from the enemy prefab array
            int number = Random.Range(0, Enemies.Length);

            // Instantiate a clone from the prefab enemies at the previously generated position
            Instantiate(Enemies[number], GenerateSpawnPosition(), Enemies[number].transform.rotation);

            spawnedEnemies++;
        }
        else
        {
            Debug.Log("No enemy for you today!");
        }
    }
}