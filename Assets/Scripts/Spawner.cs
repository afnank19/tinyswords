using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject[] enemyPool;
    [SerializeField] int[] enemyCosts = {1,2,5};
    int spawnCount = 0;
    [SerializeField] SpawnerBroker spawnerBroker;
    [SerializeField] GameObject[] spawnPoints;

    public float spawnDistance = 1f;

    public void SpawnEnemyAtOffset(Vector3 center)
    {
        // Generate a random offset for x and y in the range [1, 8]
        float offsetX = Random.Range(0f, 1f);
        float offsetY = Random.Range(0f, 1f);
        
        // Compute the spawn position using the random offsets.
        // If you're in a 2D game, z can remain 0 (or center.z if needed).
        // For 3D games, you might want to leave z as is or adjust it differently.
        Vector3 spawnPosition = new Vector3(center.x + offsetX, center.y + offsetY, center.z);
        
        // Instantiate the goblin prefab at the computed spawn position with no rotation.
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }

    public void SpawnEnemy(Vector3 center, GameObject enemy)
    {
        // Generate a random offset for x and y in the range [1, 8]
        float offsetX = Random.Range(0f, 1f);
        float offsetY = Random.Range(0f, 1f);
        
        // Compute the spawn position using the random offsets.
        // If you're in a 2D game, z can remain 0 (or center.z if needed).
        // For 3D games, you might want to leave z as is or adjust it differently.
        Vector3 spawnPosition = new Vector3(center.x + offsetX, center.y + offsetY, center.z);
        
        // Instantiate the goblin prefab at the computed spawn position with no rotation.
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }

    void Start()
    {

    }

    // This will be called as soon as the chunk is spawned
    public void SpawnEnemies(int spawnCount)
    {
        while (spawnCount > 0)
        {
            int randIdx = Random.Range(0, enemyPool.Length);
            GameObject randEnemy = enemyPool[randIdx];
            int currEnemyCost  = enemyCosts[randIdx];

            if (currEnemyCost > spawnCount)
            {
                continue;
            }

            GameObject randSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            SpawnEnemy(randSpawnPoint.transform.position, randEnemy);
            spawnCount -= currEnemyCost;
        }

        // for (int i = 0; i < spawnCount; i++)
        // {
        //     GameObject randSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        //     SpawnEnemyAtOffset(randSpawnPoint.transform.position);
        // }
    }

    public void UpdateSpawnCount(int increment)
    {
        spawnCount += increment;
    }
}
