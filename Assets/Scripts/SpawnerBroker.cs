using UnityEngine;

public class SpawnerBroker : MonoBehaviour
{
    int spawnCount = 2;

    public int GetSpawnCount()
    {
        return spawnCount;
    }

    public void IncrementSpawnCount(int increment) 
    {
        spawnCount += increment;
    }
}
