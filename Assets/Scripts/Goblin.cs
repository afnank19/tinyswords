using UnityEngine;

public class Goblin : Enemy
{
    int damage = 10;
    int goblinHealth = 25;
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
        goblinHealth -= amount;
        ApplyKnockback(sourcePos);

        if (goblinHealth <= 0) {
            Die();
        }
    }
}
