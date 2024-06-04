using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyWave> enemyWaves; // List of enemy waves
    public float timeBetweenWaves = 10f; // Time between each wave
    public float initialDelay = 0f; // Initial delay before the first wave spawns
    public Transform[] spawnPoints; // List of spawn points

    public int totalEnemiesCount = 0; // Total number of enemies in all waves

    private int currentWaveIndex = 0; // Current wave index
    private bool isSpawning = false; // Flag to check if currently spawning
    public List<GameObject> enemiesSpawned = new List<GameObject>();

    public static EnemySpawner Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        // Calculate total enemies count
        CalculateTotalEnemiesCount();

        Level.Instance.numberOfEnemiesInThisLevel = totalEnemiesCount;

        UIManager.Instance.enemyCounterText.text = $"0 / {totalEnemiesCount}";

        StartCoroutine(SpawnEnemies(currentWaveIndex));
    }

    void CalculateTotalEnemiesCount()
    {
        foreach (var wave in enemyWaves)
        {
            foreach (var count in wave.numberOfEnemies)
            {
                totalEnemiesCount += count;
            }
        }
    }

    IEnumerator SpawnEnemies(int waveIndex)
    {
        yield return new WaitForSeconds(initialDelay);
        if (enemiesSpawned.Count == totalEnemiesCount) yield break;
        isSpawning = true;

        // Get the current wave
        EnemyWave currentWave = enemyWaves[waveIndex];

        // Ensure equal number of spawn points and enemies per wave
        if (spawnPoints.Length < currentWave.numberOfEnemies.Length)
        {
            Debug.LogWarning("Not enough spawn points for wave " + (waveIndex + 1));
            yield break;
        }

        // Loop through each enemy type in the wave
        for (int i = 0; i < currentWave.enemyPrefabs.Count; i++)
        {
            // Spawn the specified number of enemies of this type
            for (int j = 0; j < currentWave.numberOfEnemies[i]; j++)
            {
                int spawnPointIndex = j % spawnPoints.Length; // Cycle through spawn points
                Vector3 spawnPosition = spawnPoints[spawnPointIndex].position;

                // Spawn enemy at the selected spawn point
                GameObject enemy = Instantiate(currentWave.enemyPrefabs[i], spawnPosition, Quaternion.identity);
                enemiesSpawned.Add(enemy);
                // You may want to add more configurations for enemy spawning here
            }
        }

        isSpawning = false;

        initialDelay = timeBetweenWaves;
        currentWaveIndex++;
        StartCoroutine(SpawnEnemies(currentWaveIndex));
    }
}

[System.Serializable]
public class EnemyWave
{
    public List<GameObject> enemyPrefabs; // List of enemy prefabs for this wave
    public int[] numberOfEnemies; // Number of each enemy type to spawn in this wave
}
