using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] GameObject LoseUI;
    [SerializeField] TextMeshProUGUI distanceTravelledUI;


    void Start()
    {
        PlayerController.OnDie += GameOver;
        Time.timeScale = 1f;
    }

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

    void GameOver()
    {
        if(LoseUI != null)
        {
            // LoseUI = GameObject.Find("Canvas/YouLose");
            Debug.Log(LoseUI);
            LoseUI.SetActive(true);
            Time.timeScale = 0f;

            distanceTravelledUI.text = "Distance Travelled: " + Height + "m";
        }
    }

    public void ResetScene()
    {
        int idx = SceneManager.GetActiveScene().buildIndex;
        // Reload it
        SceneManager.LoadScene(idx);
    }//
}
