using System;
using UnityEngine;

public class GoldPouchItem : MonoBehaviour
{
    [SerializeField] int goldAmount = 30;
    public static event Action<int> OnGoldPouchCollect;
    public void Collect()
    {
        // Update player gold here
        OnGoldPouchCollect.Invoke(goldAmount);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null) 
        {
            Collect();
            return;
        }
    }
}
