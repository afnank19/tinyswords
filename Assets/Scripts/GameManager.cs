using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] PlayerController player;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI healthUi;
    int Height = 0; // this is the score for the game
    // Update is called once per frame
    void Update()
    {
        Height = player.GetPlayerYPos() * 2;
        // Debug.Log(Height);
        score.text = Height.ToString() + "m";
        healthUi.text ="Health: " + player.GetPlayerHealth().ToString();
    }
}
