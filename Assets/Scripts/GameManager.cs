using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] PlayerController player;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI healthUi;
    [SerializeField] TextMeshProUGUI goldUI;
    [SerializeField] SpawnerBroker spawnerBroker;
    [SerializeField] ChunkManager chunkManager;
    [SerializeField] GameObject TutorialScreen;
    // [SerializeField] Spawner spawner;
    int Height = 0; // this is the score for the game
    int milestone = 50;

    [SerializeField] GameObject LoseUI;
    [SerializeField] TextMeshProUGUI distanceTravelledUI;
    [SerializeField] GameObject PauseScreen;

    private string TutorialKey = "TutorialSeen";

    void Start()
    {
        PlayerController.OnDie += GameOver;
        Time.timeScale = 1f;

        if (PlayerPrefs.HasKey(TutorialKey))
        {
            CloseTutorial();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSysHandler.instance.GetPauseState() == true)
        {
            if (!LoseUI.activeSelf)
            {
                PauseScreen.SetActive(true);
            }
            Time.timeScale = 0f;
        } else 
        {
            PauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }

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
            LoseUI.SetActive(true);
            // Time.timeScale = 0f;
            GameSysHandler.instance.ForcePauseGame();

            distanceTravelledUI.text = "Distance Travelled: " + Height + "m";
        }
    }

    public void ResetScene()
    {
        int idx = SceneManager.GetActiveScene().buildIndex;
        // Reload it
        SceneManager.LoadScene(idx);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        GameSysHandler.instance.TogglePause();
    }

    public void CloseTutorial()
    {
        TutorialScreen.SetActive(false);
        PlayerPrefs.SetInt(TutorialKey, 1);
    }
}
