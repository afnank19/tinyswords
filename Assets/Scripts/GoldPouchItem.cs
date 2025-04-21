using System;
using UnityEngine;

public class GoldPouchItem : MonoBehaviour
{
    [SerializeField] int goldAmount = 10;
    public static event Action<int> OnGoldPouchCollect;
    [SerializeField] private AudioClip collectSound;
    public void Collect()
    {
        // Update player gold here
        SoundFXManager.instance.PlaySoundFXClip(collectSound, transform, 0.7f);
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
