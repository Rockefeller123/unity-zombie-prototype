using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    public int currentWave = 0;
    public int zombiesPerWave = 5;
    public float timeBetweenWaves = 5f;

    [Header("Zombie")]
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;

    private int zombiesAlive = 0;
    private bool waveInProgress = false;

    private void Start()
    {
        StartNextWave();
    }

    private void Update()
    {
        if (waveInProgress && zombiesAlive <= 0)
        {
            waveInProgress = false;
            Invoke(nameof(StartNextWave), timeBetweenWaves);
        }
    }

    void StartNextWave()
    {
        currentWave++;
        waveInProgress = true;

        int zombiesToSpawn = zombiesPerWave + currentWave;
        zombiesAlive = zombiesToSpawn;

        for (int i = 0; i < zombiesToSpawn; i++)
        {
            SpawnZombie();
        }

        Debug.Log("Wave " + currentWave + " started with " + zombiesToSpawn + " zombies");
    }

    void SpawnZombie()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("Spawning zombie at " + spawnPoint.position);

    }

    public void OnZombieKilled()
    {
        zombiesAlive--;
    }
}

