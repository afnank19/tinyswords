using UnityEngine;

public class EnemyAttackBox : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] PlayerController player;
    BoxCollider2D bc;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>(); 
    }

    public void EnableAttackBox() 
    {
        bc.enabled = true;
    }

    public void DisableAttackBox() 
    {
        bc.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<PlayerController>();

        if (player != null) {
            // player.TakeDamage(10);
            // Debug.Log("Attacking player");
            // I think we should just trigger the animation here, 
            // and the animation can then call events on the enemy, which in turn will damage the player
            enemy.TriggerAttack(); // could pass the damage
        }
    }

    public void DamagePlayer() {
        player.TakeDamage(5);
    }
}
 