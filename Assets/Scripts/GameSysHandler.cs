using UnityEngine;

public class GameSysHandler : MonoBehaviour
{
    public static GameSysHandler instance;

    private bool isPaused = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void TogglePause()
    {
        // Debug.Log("Game is paused lol");
        isPaused = !isPaused;
        Debug.Log(isPaused);
    }

    public void ForcePauseGame()
    {
        isPaused = true;
    }

    public bool GetPauseState()
    {
        return isPaused;
    }
}
