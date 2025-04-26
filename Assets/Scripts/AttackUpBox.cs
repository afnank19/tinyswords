using UnityEngine;

public class AttackUpBox : MonoBehaviour
{
    int damage = 5;
    BoxCollider2D bc;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>(); 
        DisableAttackBox();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null) {
            enemy.TakeDamage(damage, transform.parent.localPosition);
        }
    }

    public void EnableAttackBox() 
    {
        bc.enabled = true;
    }

    public void DisableAttackBox() 
    {
        bc.enabled = false;
    }

    public void IncrementDamage(int newDamage) 
    {
        damage += newDamage;
    }
}
