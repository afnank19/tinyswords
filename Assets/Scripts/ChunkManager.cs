using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] GameObject spawnChunk;
    [SerializeField] GameObject[] chunks;
    float[] chunkWeights = {3f,2f,1f ,2f,1f};
    [SerializeField] PlayerController player;
    float spawnDist = 9.64f;
    float chunkSize = 9.64f;
    Dictionary<Vector3, GameObject> chunkHistory = new();

    [SerializeField] GameObject[] leftChunks;
    [SerializeField] GameObject[] rightChunks;
    float[] leftChunkWeights = {1f};
    float[] rightChunkWeights = {1f};
    bool isLeft = true;
     

    void Start()
    {
        Debug.Log(player.transform.position);
        
        // Vector3 currPlayerChunk = GetChunkCoordFromPosition(player.transform.position);
        
        Vector3 currChunk = new(0, 0, 0);

            if (!chunkHistory.ContainsKey(currChunk))
            {
                Vector3 spawnPos = currChunk * spawnDist; // could use chunk size too
                spawnPos = new(spawnPos.x - spawnDist/2, spawnPos.y - spawnDist/2); 
                // ^^^adding a litte offset cuz the player is in the middle(0,0) of the screen

                GameObject spawnedChunk = Instantiate(spawnChunk, spawnPos, Quaternion.identity);
                chunkHistory.Add(currChunk, spawnedChunk);
            }
    }

    void Update()
    {

        Vector3 currPlayerChunk = GetChunkCoordFromPosition(player.transform.position);
        // CODE BELOW IS FOR AN INFINITE WORLD
        // Debug.Log(currPlayerChunk);
        // for (int x = -1; x <= 1; x++) 
        // {
        //     for (int y = -1; y <= 1; y++)
        //     {
        //         Vector3 currChunk = new(currPlayerChunk.x + x, currPlayerChunk.y + y, 0);

        //         if (!chunkHistory.ContainsKey(currChunk))
        //         {
        //             Vector3 spawnPos = currChunk * spawnDist; // could use chunk size too
        //             spawnPos = new(spawnPos.x + 4.32f, spawnPos.y + 4.32f);

        //             // Get a random chunk here!
        //             GameObject chunk = GetWeightedRandomItem(chunks, chunkWeights);

        //             GameObject spawnedChunk = Instantiate(chunk, spawnPos, Quaternion.identity);
        //             chunkHistory.Add(currChunk, spawnedChunk);
        //         }
        //     }
        // }
        // int x = 0;

        // CODE BELOW IS FOR AND INFINITE VERTICAL WORLD
        for (int y = 0; y <= 1; y++)
        {
            Vector3 currChunk = new(0, currPlayerChunk.y + y, 0);

            if (!chunkHistory.ContainsKey(currChunk))
            {
                Vector3 spawnPos = currChunk * spawnDist; // could use chunk size too
                spawnPos = new(spawnPos.x - spawnDist/2, spawnPos.y - spawnDist/2); 
                // ^^^adding a litte offset cuz the player is in the middle(0,0) of the screen

                // Get a random chunk here!
                GameObject chunk;
                if (isLeft) {
                    chunk = GetWeightedRandomItem(leftChunks, leftChunkWeights);
                    isLeft = false;
                } else {
                    chunk = GetWeightedRandomItem(rightChunks, rightChunkWeights);
                    isLeft = true;
                }


                GameObject spawnedChunk = Instantiate(chunk, spawnPos, Quaternion.identity);
                chunkHistory.Add(currChunk, spawnedChunk);
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
