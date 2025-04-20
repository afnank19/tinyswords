using UnityEngine;

public class Pawn : Enemy
{
    int pawnHealth = 10;
    int damage = 5;
    float pawnMoveSpeed = 1.5f;

    public override void Attack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.TakeDamage(damage);
        eab.EnableAttackBox();
    }

    public override void TakeDamage(int amount, Vector2 sourcePos)
    {
        pawnHealth -= amount;
        ApplyKnockback(sourcePos);

        if (pawnHealth <= 0) {
            Die();
        }
    }

    public override void Move(Vector2 moveInput)
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

            rb.linearVelocity = moveInput * pawnMoveSpeed;
        }
    }
}
