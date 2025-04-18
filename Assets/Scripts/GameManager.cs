using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] PlayerController player;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI healthUi;
    [SerializeField] TextMeshProUGUI goldUI;
    [SerializeField] SpawnerBroker spawnerBroker;
    [SerializeField] ChunkManager chunkManager;
    // [SerializeField] Spawner spawner;
    int Height = 0; // this is the score for the game
    int milestone = 50;

    // Update is called once per frame
    void Update()
    {
        Height = player.GetPlayerYPos() * 2;

        if (Height > milestone)
        {
            Debug.Log("Upgrade difficulty");
            // spawnerBroker.IncrementSpawnCount(1);
            chunkManager.IncrementSpawnCount(1);
            milestone += 50;
        }

        // Debug.Log(Height);
        score.text = Height.ToString() + "m";
        healthUi.text = player.GetPlayerHealth().ToString();
        goldUI.text = player.GetPlayerGold().ToString();
    }
}
