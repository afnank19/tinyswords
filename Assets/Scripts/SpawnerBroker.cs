using UnityEngine;

public class SpawnerBroker : MonoBehaviour
{
    [SerializeField] int spawnCount = 2;

    void Start()
    {
        spawnCount = 2;
    }

    public int GetSpawnCount()
    {
        return spawnCount;
    }

    public void IncrementSpawnCount(int increment) 
    {
        spawnCount += increment;
    }
}
