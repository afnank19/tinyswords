using UnityEngine;

public class Pawn : Enemy
{
    int pawnHealth = 10;
    int damage = 5;

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
}
