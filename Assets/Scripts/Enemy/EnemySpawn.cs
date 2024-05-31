using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefab;
    public float spawnRate = 4f; // How often to spawn an enemy
    public float spawnRadius = 80f; // Radius around the spawner where enemies can appear

    private float nextSpawnTime;
    [SerializeField]
    private float currentSpawnRate;
    private float minimumSpawnRate = 1.5f;
    private float rateDecay = 0.001f;

    void Start()
    {
        currentSpawnRate = spawnRate;
        nextSpawnTime = Time.time + spawnRate; // Initialize the next spawn time
    }
    private void FixedUpdate()
    {
        currentSpawnRate = Mathf.Max(minimumSpawnRate, spawnRate - rateDecay * Time.timeSinceLevelLoad);
    }

    void Update()
    {
        
        // Check if it's time to spawn a new enemy
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            SpawnEnemy();
            nextSpawnTime = Time.time + currentSpawnRate; // Reset the next spawn time
        }
    }

    void SpawnEnemy()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad; // Random angle in radians
        Vector2 spawnPosition = new Vector2(
            Mathf.Cos(angle),
            Mathf.Sin(angle)
        ) * spawnRadius + (Vector2)transform.position;

        int enemyIndex = Random.Range(0, enemyPrefab.Length);
        GameObject enemy =  Instantiate(enemyPrefab[enemyIndex], spawnPosition, Quaternion.identity);
        enemy.SetActive(true);
        Debug.Log("Enemy spawned at: " + spawnPosition);
    }
}