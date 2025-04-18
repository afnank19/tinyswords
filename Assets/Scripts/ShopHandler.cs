using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] GameObject shopUI;
    [SerializeField] TextMeshProUGUI healCostUI;
    [SerializeField] TextMeshProUGUI upgradeCostUI;
    [SerializeField] Button healButton;
    [SerializeField] Button upgradeButton;
    [SerializeField] PlayerController player;

    private int healCost = 25;
    private int dmgUpgradeCost = 100;
    int healing = 10;
    int damangeIncrement = 5;

    public void CloseShop() {
        shopUI.SetActive(false);
    }

    public void OpenShop()
    {
        RefreshCostUI();
        shopUI.SetActive(true);
        checkAvailable();
    }

    public void HealPlayer()
    {
        player.HealPlayer(healing, healCost);
        checkAvailable();
        healCost += 5;
        RefreshCostUI();
    }

    public void UpgradePlayerDamagae() 
    {
        player.UpgradeDamage(damangeIncrement, dmgUpgradeCost);
        checkAvailable();
        dmgUpgradeCost += 10;
        RefreshCostUI();
    }

    void OnEnable()
    {
        Debug.Log("Shop enabled");
        checkAvailable();
    }

    private void checkAvailable()
    {
        int gold = player.GetPlayerGold();
        int maxHealth = player.GetMaxHealth();
        int health = player.GetPlayerHealth();
        if (gold < healCost || health == maxHealth) 
        {
            healButton.interactable = false;
        } else {
            healButton.interactable = true;
        }

        if (gold < dmgUpgradeCost) 
        {
            upgradeButton.interactable = false;
        } else {
            upgradeButton.interactable = true;
        }
    }

    private void RefreshCostUI()
    {
        healCostUI.text = healCost.ToString();
        upgradeCostUI.text = dmgUpgradeCost.ToString();
    }
}
