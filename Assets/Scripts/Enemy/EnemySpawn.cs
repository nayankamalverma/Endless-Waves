using UnityEditor;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefab;
    [SerializeField]
    private float currentSpawnRate;
    [SerializeField]
    private Transform enemyParent;
    [SerializeField]
    private int enemyPerSpawn=2;
    
    public float spawnRate = 4f; // How often to spawn an enemy
    public float spawnRadius = 80f; // Radius around the spawner where enemies can appear
    private float minimumSpawnRate = 1.5f;
    private float rateDecay = 0.001f;
    private float nextSpawnTime;

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
        if (Time.time >= nextSpawnTime)
        {
            for (int i = 0; i < enemyPerSpawn; i++) SpawnEnemy();
            nextSpawnTime = Time.time + currentSpawnRate; // Reset the next spawn time
        }
    }

    void SpawnEnemy()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad; // Random angle in radians
        Vector2 spawnPosition = new Vector2(
            Mathf.Cos(angle),
            Mathf.Sin(angle)
        ) * spawnRadius + (Vector2)enemyParent.position;

        int enemyIndex = Random.Range(0, enemyPrefab.Length);
        GameObject enemy =  Instantiate(enemyPrefab[enemyIndex], spawnPosition, Quaternion.identity , enemyParent);
        enemy.SetActive(true);
    }

    public void PauseEnemies()
    {
        foreach(Transform child in enemyParent)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void ResumeEnemies()
    {
        foreach (Transform child in enemyParent)
        {
            child.gameObject.SetActive(true);
        }
    }
}