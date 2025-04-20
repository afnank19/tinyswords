using UnityEngine;

public class EnemyKnight : Enemy
{
    int damage = 15;
    int knightHealth = 30;
    [SerializeField] AudioClip[] attackSounds;

    public override void Attack()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(attackSounds, transform, 0.6f);
        // base.Attack();
        Debug.Log("Child Attack");
        // eab.DamagePlayer();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.TakeDamage(damage);
        // playerScript.TakeDamage(damage);
        eab.EnableAttackBox();
    }

    public override void TakeDamage(int amount, Vector2 sourcePos)
    {
        knightHealth -= amount;
        ApplyKnockback(sourcePos);

        if (knightHealth <= 0) {
            Die();
        }
    }
}
