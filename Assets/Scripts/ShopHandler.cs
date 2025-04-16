using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] GameObject shopUI;
    public void CloseShop() {
        shopUI.SetActive(false);
    }
}
