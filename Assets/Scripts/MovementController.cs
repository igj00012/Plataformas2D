using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Punch Settings")]
    [SerializeField] Transform punchHit;
    [SerializeField] float punchHitDuration = 0.25f;

    [Header("Movement Settings")]
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float jumpForce = 10f;

    Rigidbody2D rb2D;
    Animator animator;
    SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected Vector2 desiredMove = Vector2.zero;
    protected bool mustJump = false;
    protected bool mustPunch = false;
    protected bool criticalHit = false;
    protected virtual void Update()
    {
        rb2D.linearVelocityX = desiredMove.x * walkSpeed;

        if (desiredMove.x != 0f)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        if (mustPunch)
        {
            if (criticalHit)
            {
                animator.SetTrigger("CriticalAttack");
                criticalHit = false;
                mustPunch = false;
                return;
            }
            animator.SetTrigger("PerformPunch");
            mustPunch = false;
        }

        if (desiredMove.x < 0f)
        {
            //spriteRenderer.flipX = true;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (desiredMove.x > 0f)
        {
            //spriteRenderer.flipX = false;
            transform.localScale = Vector3.one;
        }

        if (mustJump)
        {
            rb2D.linearVelocityY = jumpForce;
            mustJump = false;
        }
    }

    protected void PerformPunch()
    {
        mustPunch = true;
        punchHit.gameObject.SetActive(true);
        Invoke(nameof(DeactivatePunchHit), punchHitDuration);
    }

    void DeactivatePunchHit()
    {
        punchHit.gameObject.SetActive(false);
    }
    
    public virtual void NotifyHit(Hitbox2D hitbox)
    {
        Destroy(gameObject);
    }
}
