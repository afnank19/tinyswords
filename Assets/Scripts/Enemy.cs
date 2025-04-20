using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 10; // set the health for each enemy in the prefab
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] float knockbackForce = 10.5f; // 2.5
    // [SerializeField] float knockbackDuration = 0.2f;
    private readonly float moveSpeed = 1f;
    [SerializeField] public Animator anim;
    [SerializeField] public SpriteRenderer sr;
    [SerializeField] public EnemyAttackBox eab;
    [SerializeField] public PlayerController player;
    float knockbackDuration = 0.35f; // 0.25
    float knockbackTimer = 0.0f;
    public bool knockedBack;

    [Header("Loot")]
    public List<LootItem> lootItems = new();

    void Update()
    {
        if (knockedBack) {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                knockedBack = false; // Knockback has ended
            }
        }
    }



    public virtual void TakeDamage(int amount, Vector2 sourcePos) 
    {
        health -= amount;
        ApplyKnockback(sourcePos);

        if (health <= 0) {
            Die();
        }
    }

    public void Die() 
    {
        foreach (LootItem lootItem in lootItems) 
        {
            if (Random.Range(0f,100f) <= lootItem.dropChance)
            {
                InstantiateLoot(lootItem.itemPrefab);
            }
            break;
        }

        Destroy(gameObject);
        // and other things, maybe spawn a resource
    }

    void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }
    }

    public void ApplyKnockback(Vector2 sourcePosition)
    {
        knockedBack = true;
        knockbackTimer = knockbackDuration;
        Vector2 direction = (rb.position - sourcePosition).normalized;
        
        // Apply an impulse force in that direction
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }

    public virtual void Move(Vector2 moveInput) 
    {
        if (!knockedBack) 
        {
            if (moveInput != Vector2.zero) {
                anim.SetBool("isMoving", true);
            } else {
                anim.SetBool("isMoving", false);
            }

            if (moveInput.x > 0.0f) {
                sr.flipX = false;
            } else if (moveInput.x < 0.0f) {
                sr.flipX = true;
            }

            rb.linearVelocity = moveInput * moveSpeed;
        }
    }

    public void TriggerAttack(PlayerController playerRef) {
        player = playerRef;

        anim.SetTrigger("Attack");
        eab.DisableAttackBox();
    }

    public virtual void Attack() {
        // eab.DamagePlayer();
        Debug.Log("Parent attack");
        player.TakeDamage(5);
        eab.EnableAttackBox();
    }
}
