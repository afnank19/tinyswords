using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 10; // set the health for each enemy in the prefab
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float knockbackForce = 2.5f;
    // [SerializeField] float knockbackDuration = 0.2f;
    private readonly float moveSpeed = 1f;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] EnemyAttackBox eab;
    float knockbackDuration = 0.25f;
    float knockbackTimer = 0.0f;
    bool knockedBack;

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



    public void TakeDamage(int amount, Vector2 sourcePos) 
    {
        health -= amount;
        ApplyKnockback(sourcePos);

        if (health <= 0) {
            Die();
        }
    }

    public void Die() 
    {
        Destroy(gameObject);
        // and other things, maybe spawn a resource
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

    public void Move(Vector2 moveInput) 
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

    public void TriggerAttack() {
        anim.SetTrigger("Attack");
        eab.DisableAttackBox();
    }

    public void Attack() {
        eab.DamagePlayer();
        eab.EnableAttackBox();
    }
}
