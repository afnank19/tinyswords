using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private int maxHealth = 30;
    private int health;
    private int gold = 25;
    private float moveSpeed = 1.5f;
    public Rigidbody2D rb;
    public Vector2 moveInput;
    SpriteRenderer sr;
    Animator anim;
    bool canMove = true;
    Vector2 lastMoveInput;
    /*---------------------SOUNDS---------------------*/
    [SerializeField] AttackBox ab;
    [SerializeField] private AudioClip[] walkSounds;
    [SerializeField] private AudioClip[] attackSounds;
    [SerializeField] private AudioClip[] dmgSounds;
    [SerializeField] private AudioClip pickupSound;
    /*---------------------EVENTS---------------------*/
    public static event Action OnDie;
    /*---------------------MECHANICS---------------------*/
    [Header("Dash Mechanic")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 0.25f;
    [SerializeField] float dashCooldown  = 1f;

    bool isDashing = false;
    bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        health = maxHealth;

        GoldPouchItem.OnGoldPouchCollect += AddToGold;
    }

    // Update is called once per frame
    void Update()
    {
        if ( isDashing ) return;

        if (canMove) {
            if (moveInput != Vector2.zero) {
                anim.SetBool("isMoving", true);
                SoundFXManager.instance.PlaySoundFXClipAfterAnother(walkSounds, transform, 0.4f);
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

    public void Move(InputAction.CallbackContext context) 
    {
        if (GameSysHandler.instance.GetPauseState() != false) return;

        moveInput = context.ReadValue<Vector2>();
    }

    public void TriggerDash(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (!canDash) return;

        StartCoroutine(Dash());
    }

    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.linearVelocity = moveInput * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void TriggerAttack(InputAction.CallbackContext context) 
    {
        if (context.performed && GameSysHandler.instance.GetPauseState() == false) {
            anim.SetTrigger("Attack");
            Debug.Log("Attacking mordoria");
        }
    }

    public void TakeDamage(int amount) {
        if (isDashing) return;

        health -= amount;
        SoundFXManager.instance.PlayRandomSoundFXClip(dmgSounds, transform, 0.7f);

        Debug.Log(health);
        if (health <= 0) {
            OnDie.Invoke();
        }
    }

    // This method is triggered by an anim event so that it matches up with the sword swing
    public void Attack() {
        SoundFXManager.instance.PlayRandomSoundFXClip(attackSounds, transform, 1f);
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

    public void AddToGold(int amount)
    {
        // if (gameObject != null)
        // {
        //     SoundFXManager.instance.PlaySoundFXClip(pickupSound, transform, 0.7f);
        // }
        gold += amount;
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
