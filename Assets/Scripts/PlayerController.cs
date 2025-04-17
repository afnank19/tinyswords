using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private int maxHealth = 30;
    private int health;
    private int gold = 100;
    private float moveSpeed = 1.5f;
    public Rigidbody2D rb;
    public Vector2 moveInput;
    SpriteRenderer sr;
    Animator anim;
    bool canMove = true;
    Vector2 lastMoveInput;

    [SerializeField] AttackBox ab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) {
            if (moveInput != Vector2.zero) {
                anim.SetBool("isMoving", true);
            } else {
                anim.SetBool("isMoving", false);
            }

            if (moveInput.x > 0) {
                sr.flipX = false;
                ab.FlipAttackBoxRight();
            } else if (moveInput.x < 0) {
                sr.flipX = true;
                ab.FlipAttackBoxLeft();
            }

            rb.linearVelocity = moveInput * moveSpeed;
        }
    }

    public void Move(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
    }

    public void TriggerAttack(InputAction.CallbackContext context) 
    {
        if (context.performed) {
            anim.SetTrigger("Attack");
            Debug.Log("Attacking mordoria");
        }
    }

    public void TakeDamage(int amount) {
        health -= amount;

        Debug.Log(health);
        if (health <= 0) {
        }
    }

    // This method is triggered by an anim event so that it matches up with the sword swing
    public void Attack() {
        ab.EnableAttackBox();
    }

    public void LockMovement() {
        canMove = false;
        lastMoveInput = moveInput;
        // moveInput = Vector2.zero;
        rb.linearVelocity = Vector2.zero;

    }
    public void UnlockMovement() {
        canMove = true;
        rb.linearVelocity = moveInput * moveSpeed;

        // Attack logic
        ab.DisableAttackBox();
    }

    public int GetPlayerYPos() {
        return (int)transform.position.y;
    }

    public int GetPlayerHealth() 
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetPlayerGold()
    {
        return gold;
    }

    public void HealPlayer(int healAmt, int cost)
    {
        if (health >= maxHealth)
        {
            return;
        }

        if (gold < cost) 
        {
            return;
        }

        gold -= cost;
        health += healAmt;
        if (health > maxHealth) 
        {
            health = maxHealth;
        }
    }

    public void UpgradeDamage(int damangeIncrement, int cost)
    {
        if (gold < cost) 
        {
            return;
        }

        gold -= cost;
        ab.IncrementDamage(damangeIncrement);
    }
}
