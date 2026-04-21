using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    public int currentWave = 0;
    public int zombiesPerWave = 5;
    public float timeBetweenWaves = 5f;

    [Header("Spawn Anti-Stacking")]
    public float spawnClearRadius = 1.2f;     // how close is "too close"
    public int spawnTryCount = 10;            // how many attempts before giving up
    public LayerMask zombieLayerMask;         // set this to the Zombie layer


    [Header("Zombie")]
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;
    public void OnZombieKilled()
{
    zombiesAlive--;
    Debug.Log("Zombie killed. Zombies alive: " + zombiesAlive);
}


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
        if (spawnPoints == null || spawnPoints.Length == 0 || zombiePrefab == null)
            return;

        for (int attempt = 0; attempt < spawnTryCount; attempt++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Check for existing zombies near this spawn point
            Collider[] hits = Physics.OverlapSphere(
                spawnPoint.position,
                spawnClearRadius,
                zombieLayerMask
            );

            // If nothing found in the radius, it's safe to spawn here
            if (hits.Length == 0)
            {
                Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
                Debug.Log($"Spawning zombie at {spawnPoint.position} (attempt {attempt + 1})");
                return;
            }
        }

    Debug.LogWarning("No clear spawn point found. Try adding more spawn points or lowering spawnClearRadius.");
}



}

