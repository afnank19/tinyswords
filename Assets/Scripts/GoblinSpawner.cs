using UnityEngine;

public class GoblinSpawner : MonoBehaviour
{
    [SerializeField] GameObject goblin;

    public float spawnDistance = 1f;

    public void SpawnGoblinAtPoint(Vector3 center)
    {
        // Generate a random angle in degrees (0 to 360)
        float angle = Random.Range(0f, 360f);

        // Convert the angle to radians since Mathf.Cos/Sin work with radians.
        float radian = angle * Mathf.Deg2Rad;

        // Compute the spawn position using basic trigonometry
        Vector3 spawnPosition = new(
            center.x + Mathf.Cos(radian) * spawnDistance,
            center.y,
            center.z + Mathf.Sin(radian) * spawnDistance
        );

        // Instantiate the goblin prefab at the spawn position with no rotation.
        Instantiate(goblin, spawnPosition, Quaternion.identity);
    }

    public void SpawnGoblinWithRandomOffset(Vector3 center)
    {
        // Generate a random offset for x and y in the range [1, 8]
        float offsetX = Random.Range(1f, 8f);
        float offsetY = Random.Range(1f, 8f);
        
        // Compute the spawn position using the random offsets.
        // If you're in a 2D game, z can remain 0 (or center.z if needed).
        // For 3D games, you might want to leave z as is or adjust it differently.
        Vector3 spawnPosition = new Vector3(center.x + offsetX, center.y + offsetY, center.z);
        
        // Instantiate the goblin prefab at the computed spawn position with no rotation.
        Instantiate(goblin, spawnPosition, Quaternion.identity);
    }

    void Start()
    {
        SpawnGoblinWithRandomOffset(transform.localPosition);
        SpawnGoblinWithRandomOffset(transform.localPosition);
    }
}
