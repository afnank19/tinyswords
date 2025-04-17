using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    bool shopInRange = false;
    // [SerializeField] GameObject shopUI; 
    [SerializeField] ShopHandler sh;
    SpriteRenderer interactableIcon;

    void Start()
    {
        interactableIcon = GetComponent<SpriteRenderer>();
        interactableIcon.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If the interactable thing is a shop!
        if (collision.CompareTag("Shop")) 
        {
            Debug.Log("Hey, A shop");
            shopInRange = true;
            interactableIcon.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        shopInRange = false;
        interactableIcon.enabled = false;
    }

    public void InteractWithShop(InputAction.CallbackContext context) {
        if (shopInRange) 
        {
            Debug.Log("Interacting wooohooo");
            // shopUI.SetActive(true);
            sh.OpenShop();
            return;
        } 
    }
}
