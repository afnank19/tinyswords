using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] GameObject chunk;
    [SerializeField] GameObject[] chunks;
    float[] chunkWeights = {4f,2f,2f};
    [SerializeField] PlayerController player;
    float spawnDist = 9.64f;
    float chunkSize = 9.64f;
    Dictionary<Vector3, GameObject> chunkHistory = new(); 

    void Start()
    {
        Debug.Log(player.transform.position);
        // Debug.Log(GetChunkCoordFromPosition(player.transform.position));

        // Vector3 spawnPos = player.transform.position + new Vector3(9.64f,0);
        // Vector3 sp2 = player.transform.position + new Vector3(9.64f,9.64f);
        // Vector3 sp3 = player.transform.position + new Vector3(0,9.64f);

        // Instantiate(chunk, player.transform.position, Quaternion.identity);
        // Instantiate(chunk, spawnPos, Quaternion.identity);
        // Instantiate(chunk, sp2, Quaternion.identity);
        // Instantiate(chunk, sp3, Quaternion.identity);
    }

    void Update()
    {

        Vector3 currPlayerChunk = GetChunkCoordFromPosition(player.transform.position);
        // Debug.Log(currPlayerChunk);
        for (int x = -1; x <= 1; x++) 
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector3 currChunk = new(currPlayerChunk.x + x, currPlayerChunk.y + y, 0);

                if (!chunkHistory.ContainsKey(currChunk))
                {
                    Vector3 spawnPos = currChunk * spawnDist; // could use chunk size too

                    // Get a random chunk here!
                    GameObject chunk = GetWeightedRandomItem(chunks, chunkWeights);

                    GameObject spawnedChunk = Instantiate(chunk, spawnPos, Quaternion.identity);
                    chunkHistory.Add(currChunk, spawnedChunk);
                }
            }
        }

    }


    Vector3 GetChunkCoordFromPosition(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / chunkSize);
        int y = Mathf.FloorToInt(position.y / chunkSize);
        return new Vector3(x, y, 0);
    }

    public static T GetWeightedRandomItem<T>(T[] items, float[] weights)
    {
        if (items == null || weights == null || items.Length != weights.Length || items.Length == 0)
        {
            Debug.LogError("Items and weights must be non-null and of equal, non-zero length.");
            return default;
        }
        
        // Calculate the total weight
        float totalWeight = 0f;
        foreach (float weight in weights)
        {
            totalWeight += weight;
        }
        
        // Generate a random number between 0 and totalWeight
        float randomNumber = Random.Range(0f, totalWeight);
        
        // Determine the item that corresponds to this random number
        for (int i = 0; i < weights.Length; i++)
        {
            if (randomNumber < weights[i])
            {
                return items[i];
            }
            randomNumber -= weights[i];
        }

        // Fallback, should never really happen if weights and items are correct.
        return items[items.Length - 1];
    }
}
